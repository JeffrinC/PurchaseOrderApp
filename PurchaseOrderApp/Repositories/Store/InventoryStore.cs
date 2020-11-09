using PurchaseOrderApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PurchaseOrderApp.Repositories.Store
{
    public class InventoryStore : IInventoryStore
    {
        private Inventory _inventory = new Inventory();

        private readonly string _filepath;

        private readonly object _fileLocker = new object();

        public InventoryStore(string filepath)
        {
            _filepath = filepath;
        }

        Inventory IInventoryStore.All()
        {
            return _inventory;
        }

        bool IInventoryStore.Create(PurchaseOrderItem created)
        {
            created.Id = new Guid();
            lock (_inventory)
                _inventory.PurchaseOrderItems.Add(created);

            return true;
        }

        public bool TryRemove(PurchaseOrderItem removed)
        {
            lock (_inventory)
            {
                var match = _inventory.PurchaseOrderItems.Where(p => p.Id == removed.Id);

                if (match == null)
                    return false;

                _inventory.PurchaseOrderItems.Remove(match.Single());
            }

            return true;
        }

        public bool TryUpdate(PurchaseOrderItem updated)
        {
            lock (_inventory)
            {
                IEnumerable<PurchaseOrderItem> match = _inventory.PurchaseOrderItems.Where(u => u.Id == updated.Id);

                if (match.Count() != 1)
                    return false;

                var old = match.Single();

                updated.Id = old.Id;

                _inventory.PurchaseOrderItems.Remove(match.Single());

                _inventory.PurchaseOrderItems.Add(updated);
            }

            return true;
        }

        public void Save()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string serialized = "";

            lock (_inventory)
                serialized = JsonSerializer.Serialize(_inventory, options);

            WriteInFile(serialized, _filepath);
        }

        private void WriteInFile(string content, string filepath)
        {
            lock (_fileLocker)
            {
                using (FileStream fs = File.Create(filepath))
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.Write(content);
                }
            }
        }

        private string ReadJsonFromFile(string jsonPath)
        {
            lock (_fileLocker)
            {
                using (FileStream fs = File.OpenRead(jsonPath))
                using (TextReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }


    }
}

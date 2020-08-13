using LiteDB;
using Recursive.Data;
using Recursive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recursive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Category> categories;

        private void Form1_Load(object sender, EventArgs e)
        {
            //first run must seed data
            //SeedData();

            //using data
            using (var db = new LiteDatabase(@".\MyData.db"))
            {
                var col = db.GetCollection<Category>("categories");
                categories = col.Query().ToList();
            }

            //fill tree
            PopulateTree();
            //end of fill expand all categories
            twCats.ExpandAll();
        }

        void PopulateTree(TreeNode parentNode = null, int? parentId = null)
        {
            foreach (var category in categories.Where(p => p.ParentId == parentId))
            {
                TreeNode node = new TreeNode(category.Name);
                node.Tag = category
                    .Products.ToList();
                if (parentNode == null)
                    twCats.Nodes.Add(node);
                else
                    parentNode.Nodes.Add(node);

                PopulateTree(node, category.Id);
            }
        }

        void SeedData()
        {
            using (var db = new LiteDatabase(@".\MyData.db"))
            {
                var col = db.GetCollection<Category>("categories");
                var cat1 = new Category
                {
                    Id = 1,
                    Name = "Cat 1"
                };
                var cat2 = new Category
                {
                    Id = 2,
                    Name = "Cat 2",
                    ParentId = 1,
                    Products = new List<Product>()
                    {
                        new Product(){Id = 1, Name = "Pro 2-1",Price = 100,Amount = 70},
                        new Product(){Id = 2, Name = "Pro 2-2",Price = 200,Amount = 80},
                    }
                };
                var cat3 = new Category
                {
                    Id = 3,
                    Name = "Cat 3",
                    ParentId = 2,
                    Products = new List<Product>()
                    {
                        new Product(){Id = 3, Name = "Pro 3-1",Price = 300,Amount = 60},
                        new Product(){Id = 4, Name = "Pro 3-2",Price = 400,Amount = 50},
                    }
                };
                var cat4 = new Category
                {
                    Id = 4,
                    Name = "Cat 4"
                };
                var cat5 = new Category
                {
                    Id = 5,
                    Name = "Cat 5",
                    ParentId = 4,
                    Products = new List<Product>()
                    {
                        new Product(){Id = 5, Name = "Pro 5-1",Price = 100,Amount = 70},
                        new Product(){Id = 6, Name = "Pro 5-2",Price = 200,Amount = 80},
                    }
                };
                col.Insert(cat1);
                col.Insert(cat2);
                col.Insert(cat3);
                col.Insert(cat4);
                col.Insert(cat5);
            }
        }

        private void twCats_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                dataGridView1.DataSource = (List<Product>)e.Node.Tag;
            }
            else
                dataGridView1.DataSource = null;
        }
    }
}

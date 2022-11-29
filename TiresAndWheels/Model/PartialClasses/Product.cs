using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresAndWheels.Model
{
    public partial class Product
    {
        public string ImagePath { 
            get 
            {
                if (Image == null)
                {
                    return "\\Assets\\Images\\errorPicture.png";
                }
                else
                {
                    return "/Assets/Images" + Image;
                }
            } 
        }
        public string MaterialList {
            get
            {
                string materials = "Материалы: ";
                List<string> arrayMaterials = new List<string> { };
                List<ProductMaterial> arrayActivateProduct = ProductMaterial.Where(x => x.ProductID == ID).ToList();
                foreach (var item in arrayActivateProduct)
                {
                    arrayMaterials.Add(item.Material.Title.ToString());
                }
                materials += String.Join(",", arrayMaterials);
                return materials;
            }
        }
        public double CostProduct
        {
            get
            {
                double costProduct = 0;
                List<ProductMaterial> arrayActivateProduct = ProductMaterial.Where(x => x.ProductID == ID).ToList();

                foreach (var item in arrayActivateProduct)
                {
                    costProduct += Convert.ToDouble(item.Count) * Convert.ToDouble(item.Material.Cost);
                }
                
                return costProduct;
            }
        }
    }
}

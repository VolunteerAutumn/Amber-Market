using Market.Models;

namespace Market.Data
{
    public static class Seeder
    {
        public static void Seed(MarketDbContext context)
        {
            if (context.Products.Any())
                return;

            var categories = new List<Category>
            {
                new Category { Name = "AK-47" },
                new Category { Name = "M4A1-S" },
                new Category { Name = "AWP" },
                new Category { Name = "M4A4" },
                new Category { Name = "USP-S" },
                new Category { Name = "Glock-18" },
                new Category { Name = "Desert Eagle" },
                new Category { Name = "P250" },
                new Category { Name = "MP9" },
                new Category { Name = "FAMAS" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new List<Product>
            {
                // AK-47
                new Product
                {
                    Name = "Vulcan",
                    Price = 120.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A futuristic blue and white AK-47 skin with a striking geometric design.",
                    ImagePath = "/images/skins/ak-47/vulcan.png",
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Case Hardened",
                    Price = 250.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Classified",
                    Description = "A legendary AK-47 featuring a unique hardened steel finish.",
                    ImagePath = "/images/skins/ak-47/case_hardened.png",
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Bloodsport",
                    Price = 85.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A racing-inspired AK-47 covered in aggressive red, black and white graphics.",
                    ImagePath = "/images/skins/ak-47/bloodsport.png",
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Redline",
                    Price = 55.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A sleek black AK-47 decorated with distinctive red lines.",
                    ImagePath = "/images/skins/ak-47/redline.png",
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Fire Serpent",
                    Price = 450.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "An iconic AK-47 featuring a fierce green and bronze serpent.",
                    ImagePath = "/images/skins/ak-47/fire_serpent.png",
                    CategoryId = categories[0].Id
                },

                // M4A1-S
                new Product
                {
                    Name = "Printstream",
                    Price = 95.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Covert",
                    Description = "A clean white M4A1-S with futuristic black graphics.",
                    ImagePath = "/images/skins/m4a1-s/printstream.png",
                    CategoryId = categories[1].Id
                },
                new Product
                {
                    Name = "Hyper Beast",
                    Price = 70.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A colorful M4A1-S covered in a monstrous psychedelic design.",
                    ImagePath = "/images/skins/m4a1-s/hyper_beast.png",
                    CategoryId = categories[1].Id
                },
                new Product
                {
                    Name = "Blue Phosphor",
                    Price = 650.00m,
                    Condition = "Factory New",
                    Rarity = "Restricted",
                    Description = "A brilliantly polished M4A1-S with an intense blue finish.",
                    ImagePath = "/images/skins/m4a1-s/blue_phosphor.png",
                    CategoryId = categories[1].Id
                },
                new Product
                {
                    Name = "Decimator",
                    Price = 35.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A futuristic M4A1-S featuring vivid purple and cyan details.",
                    ImagePath = "/images/skins/m4a1-s/decimator.png",
                    CategoryId = categories[1].Id
                },
                new Product
                {
                    Name = "Welcome to the Jungle",
                    Price = 900.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A luxurious jungle-themed M4A1-S covered in exotic artwork.",
                    ImagePath = "/images/skins/m4a1-s/welcome_to_the_jungle.png",
                    CategoryId = categories[1].Id
                },

                // AWP
                new Product
                {
                    Name = "Dragon Lore",
                    Price = 5000.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "One of the most legendary AWP skins, featuring a golden dragon.",
                    ImagePath = "/images/skins/awp/dragon_lore.png",
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Asiimov",
                    Price = 130.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A futuristic orange, black and white AWP design.",
                    ImagePath = "/images/skins/awp/asiimov.png",
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Gungnir",
                    Price = 1800.00m,
                    Condition = "Factory New",
                    Rarity = "Covert",
                    Description = "A mythical blue AWP decorated with Norse-inspired artwork.",
                    ImagePath = "/images/skins/awp/gungnir.png",
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Atheris",
                    Price = 15.00m,
                    Condition = "Field-Tested",
                    Rarity = "Restricted",
                    Description = "A dark AWP featuring a vivid blue snake.",
                    ImagePath = "/images/skins/awp/atheris.png",
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Neo-Noir",
                    Price = 45.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Covert",
                    Description = "A stylish comic-book inspired AWP with neon artwork.",
                    ImagePath = "/images/skins/awp/neo_noir.png",
                    CategoryId = categories[2].Id
                },

                // M4A4
                new Product
                {
                    Name = "Howl",
                    Price = 3000.00m,
                    Condition = "Field-Tested",
                    Rarity = "Contraband",
                    Description = "The infamous M4A4 Howl, featuring an aggressive red wolf.",
                    ImagePath = "/images/skins/m4a4/howl.png",
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Asiimov",
                    Price = 120.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A futuristic orange, white and black M4A4.",
                    ImagePath = "/images/skins/m4a4/asiimov.png",
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Emperor",
                    Price = 110.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Covert",
                    Description = "An ornate blue M4A4 inspired by traditional playing cards.",
                    ImagePath = "/images/skins/m4a4/emperor.png",
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Temukau",
                    Price = 25.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A colorful M4A4 with anime-inspired artwork.",
                    ImagePath = "/images/skins/m4a4/temukau.png",
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Neo-Noir",
                    Price = 35.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A neon comic-style M4A4 covered in dramatic artwork.",
                    ImagePath = "/images/skins/m4a4/neo_noir.png",
                    CategoryId = categories[3].Id
                },

                // USP-S
                new Product
                {
                    Name = "Kill Confirmed",
                    Price = 80.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A dramatic USP-S featuring colorful comic-style artwork.",
                    ImagePath = "/images/skins/usp-s/kill_confirmed.png",
                    CategoryId = categories[4].Id
                },
                new Product
                {
                    Name = "Printstream",
                    Price = 55.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Covert",
                    Description = "A clean white USP-S with futuristic black details.",
                    ImagePath = "/images/skins/usp-s/printstream.png",
                    CategoryId = categories[4].Id
                },
                new Product
                {
                    Name = "Orion",
                    Price = 45.00m,
                    Condition = "Factory New",
                    Rarity = "Classified",
                    Description = "A sleek black and orange USP-S inspired by deep space.",
                    ImagePath = "/images/skins/usp-s/orion.png",
                    CategoryId = categories[4].Id
                },
                new Product
                {
                    Name = "Cyrex",
                    Price = 12.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Restricted",
                    Description = "A futuristic black, red and white USP-S.",
                    ImagePath = "/images/skins/usp-s/cyrex.png",
                    CategoryId = categories[4].Id
                },
                new Product
                {
                    Name = "Cortex",
                    Price = 10.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A pink and purple USP-S with colorful brain-inspired artwork.",
                    ImagePath = "/images/skins/usp-s/cortex.png",
                    CategoryId = categories[4].Id
                },

                // Glock-18
                new Product
                {
                    Name = "Fade",
                    Price = 900.00m,
                    Condition = "Factory New",
                    Rarity = "Restricted",
                    Description = "A beautiful Glock-18 with a bright metallic gradient.",
                    ImagePath = "/images/skins/glock-18/fade.png",
                    CategoryId = categories[5].Id
                },
                new Product
                {
                    Name = "Water Elemental",
                    Price = 20.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A blue Glock-18 decorated with a powerful water creature.",
                    ImagePath = "/images/skins/glock-18/water_elemental.png",
                    CategoryId = categories[5].Id
                },
                new Product
                {
                    Name = "Vogue",
                    Price = 12.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Classified",
                    Description = "A stylish Glock-18 featuring colorful pop-art graphics.",
                    ImagePath = "/images/skins/glock-18/vogue.png",
                    CategoryId = categories[5].Id
                },
                new Product
                {
                    Name = "Bullet Queen",
                    Price = 35.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A colorful Glock-18 featuring a striking comic-style queen.",
                    ImagePath = "/images/skins/glock-18/bullet_queen.png",
                    CategoryId = categories[5].Id
                },
                new Product
                {
                    Name = "Neo-Noir",
                    Price = 30.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A neon noir Glock-18 with comic-inspired artwork.",
                    ImagePath = "/images/skins/glock-18/neo_noir.png",
                    CategoryId = categories[5].Id
                },

                // Desert Eagle
                new Product
                {
                    Name = "Blaze",
                    Price = 700.00m,
                    Condition = "Factory New",
                    Rarity = "Restricted",
                    Description = "A classic golden Desert Eagle covered in flames.",
                    ImagePath = "/images/skins/desert_eagle/blaze.png",
                    CategoryId = categories[6].Id
                },
                new Product
                {
                    Name = "Printstream",
                    Price = 75.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Covert",
                    Description = "A luxurious white Desert Eagle with black futuristic details.",
                    ImagePath = "/images/skins/desert_eagle/printstream.png",
                    CategoryId = categories[6].Id
                },
                new Product
                {
                    Name = "Code Red",
                    Price = 40.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A striking red and black Desert Eagle.",
                    ImagePath = "/images/skins/desert_eagle/code_red.png",
                    CategoryId = categories[6].Id
                },
                new Product
                {
                    Name = "Kumicho Dragon",
                    Price = 35.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A detailed Desert Eagle decorated with an elegant dragon.",
                    ImagePath = "/images/skins/desert_eagle/kumicho_dragon.png",
                    CategoryId = categories[6].Id
                },
                new Product
                {
                    Name = "Light Rail",
                    Price = 8.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Restricted",
                    Description = "A modern black and red Desert Eagle with angular details.",
                    ImagePath = "/images/skins/desert_eagle/light_rail.png",
                    CategoryId = categories[6].Id
                },

                // P250
                new Product
                {
                    Name = "Asiimov",
                    Price = 20.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A futuristic orange and black P250.",
                    ImagePath = "/images/skins/p250/asiimov.png",
                    CategoryId = categories[7].Id
                },
                new Product
                {
                    Name = "See Ya Later",
                    Price = 15.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "A colorful P250 featuring a cartoon crocodile.",
                    ImagePath = "/images/skins/p250/see_ya_later.png",
                    CategoryId = categories[7].Id
                },
                new Product
                {
                    Name = "Muertos",
                    Price = 10.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A vibrant P250 decorated with colorful skull artwork.",
                    ImagePath = "/images/skins/p250/muertos.png",
                    CategoryId = categories[7].Id
                },
                new Product
                {
                    Name = "Mehndi",
                    Price = 5.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Classified",
                    Description = "A detailed P250 featuring intricate traditional patterns.",
                    ImagePath = "/images/skins/p250/mehndi.png",
                    CategoryId = categories[7].Id
                },
                new Product
                {
                    Name = "Franklin",
                    Price = 1.50m,
                    Condition = "Factory New",
                    Rarity = "Restricted",
                    Description = "A P250 decorated with a playful banknote-inspired design.",
                    ImagePath = "/images/skins/p250/franklin.png",
                    CategoryId = categories[7].Id
                },

                // MP9
                new Product
                {
                    Name = "Mount Fuji",
                    Price = 15.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Classified",
                    Description = "A colorful MP9 featuring a stylized Mount Fuji.",
                    ImagePath = "/images/skins/mp9/mount_fuji.png",
                    CategoryId = categories[8].Id
                },
                new Product
                {
                    Name = "Hot Rod",
                    Price = 60.00m,
                    Condition = "Factory New",
                    Rarity = "Restricted",
                    Description = "A brilliantly polished red MP9 with a sleek finish.",
                    ImagePath = "/images/skins/mp9/hot_rod.png",
                    CategoryId = categories[8].Id
                },
                new Product
                {
                    Name = "Food Chain",
                    Price = 10.00m,
                    Condition = "Field-Tested",
                    Rarity = "Restricted",
                    Description = "A colorful MP9 covered in bizarre creatures and food-chain artwork.",
                    ImagePath = "/images/skins/mp9/food_chain.png",
                    CategoryId = categories[8].Id
                },
                new Product
                {
                    Name = "Starlight Protector",
                    Price = 12.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Classified",
                    Description = "A mystical purple MP9 with a magical guardian design.",
                    ImagePath = "/images/skins/mp9/starlight_protector.png",
                    CategoryId = categories[8].Id
                },
                new Product
                {
                    Name = "Bulldozer",
                    Price = 80.00m,
                    Condition = "Factory New",
                    Rarity = "Restricted",
                    Description = "A bright yellow MP9 with a bold industrial appearance.",
                    ImagePath = "/images/skins/mp9/bulldozer.png",
                    CategoryId = categories[8].Id
                },

                // FAMAS
                new Product
                {
                    Name = "Commemoration",
                    Price = 20.00m,
                    Condition = "Field-Tested",
                    Rarity = "Covert",
                    Description = "An ornate FAMAS commemorating a historic battle.",
                    ImagePath = "/images/skins/famas/commemoration.png",
                    CategoryId = categories[9].Id
                },
                new Product
                {
                    Name = "Mecha Industries",
                    Price = 8.00m,
                    Condition = "Minimal Wear",
                    Rarity = "Classified",
                    Description = "A futuristic white FAMAS with mechanical detailing.",
                    ImagePath = "/images/skins/famas/mecha_industries.png",
                    CategoryId = categories[9].Id
                },
                new Product
                {
                    Name = "Roll Cage",
                    Price = 5.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A colorful FAMAS covered in racing-inspired graphics.",
                    ImagePath = "/images/skins/famas/roll_cage.png",
                    CategoryId = categories[9].Id
                },
                new Product
                {
                    Name = "Eye of Athena",
                    Price = 6.00m,
                    Condition = "Field-Tested",
                    Rarity = "Classified",
                    Description = "A mystical FAMAS decorated with an imposing golden eye.",
                    ImagePath = "/images/skins/famas/eye_of_athena.png",
                    CategoryId = categories[9].Id
                },
                new Product
                {
                    Name = "Djinn",
                    Price = 4.00m,
                    Condition = "Field-Tested",
                    Rarity = "Restricted",
                    Description = "A dark FAMAS featuring supernatural blue and purple artwork.",
                    ImagePath = "/images/skins/famas/djinn.png",
                    CategoryId = categories[9].Id
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
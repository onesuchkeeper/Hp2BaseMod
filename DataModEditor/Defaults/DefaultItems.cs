﻿// Hp2BaseMod 2021, By OneSuchKeeper

using System.Collections.Generic;

namespace DataModEditor.Data
{
    public static partial class Default
    {
        //ItemType.FRUIT 0
        public static Dictionary<int, string> ItemsFruit = new Dictionary<int, string>
        {
            {1, "Blueberry"},
            {10, "Lime"},
            {11, "Orange"},
            {12, "Mango"},
            {13, "Papaya"},
            {14, "Cantaloupe"},
            {15, "Peach"},
            {16, "Apple"},
            {17, "Cherry"},
            {18, "Strawberry"},
            {19, "Pomegranate"},
            {2, "Raspberry"},
            {20, "Pitaya"},
            {3, "Rambutan"},
            {4, "Grapes"},
            {5, "Plum"},
            {6, "Pear"},
            {7, "Kiwi"},
            {8, "Watermelon"},
            {9, "Carambola"},
        };

        //ItemType.SMOOTHIE 1
        public static Dictionary<int, string> ItemsSmoothie = new Dictionary<int, string>
        {
            { 21, "Talent Smoothie" },
            { 22, "Flirtation Smoothie" },
            { 23, "Romance Smoothie" },
            { 24, "Sexuality Smoothie" }
        };

        //ItemType.FOOD 2
        public static Dictionary<int, string> ItemsFood = new Dictionary<int, string>
        {
            {53, "Rice Ball"},
            {54, "Sushi"},
            {56, "Ramen"},
            {57, "Bento Box"},
            {58, "Guacamole"},
            {59, "Nachos"},
            {60, "Taco"},
            {61, "Burrito"},
            {63, "Croissant"},
            {64, "Baguette"},
            {65, "Quiche"},
            {66, "Crepe"},
            {68, "Bruschetta"},
            {70, "Ravioli"},
            {71, "Pizza"},
            {72, "Lasagna"},
            {74, "Fried Chicken"},
            {75, "Hot Dog"},
            {76, "Hamburger"},
            {77, "Corn on the Cob"},
            {78, "Shrimp"},
            {80, "Mussels"},
            {81, "Grilled Salmon"},
            {82, "Lobster Tail"},
            {83, "Cupcake"},
            {84, "Cinnamon Bun"},
            {85, "Slice of Pie"},
            {87, "Shaved Ice"},
            {88, "Candy Necklace"},
            {89, "Chocolate Drops"},
            {90, "Fudge Truffles"},
            {91, "Cotton Candy"}
        };

        //ItemType.SHOES 3
        public static Dictionary<int, string> ItemsShoes = new Dictionary<int, string>
        {
            {189, "Knitted Boots"},
            {190, "Seasonal Boots"},
            {191, "Heavy Boots"},
            {192, "Fuzzy Boots"},
            {193, "Festive Boots"},
            {194, "Elegant Peep Toes"},
            {195, "Angel Peep Toes"},
            {196, "Plaid Peep Toes"},
            {197, "Ribbon Peep Toes"},
            {198, "Leopard Peep Toes"},
            {199, "Suede Booties"},
            {200, "Striped Booties"},
            {201, "Goth Booties"},
            {202, "Abstract Booties"},
            {203, "Satanic Booties"},
            {204, "Astro Boots"},
            {205, "Sacred Boots"},
            {206, "Sherbet Boots"},
            {207, "Hydro Boots"},
            {208, "Cosmic Boots"},
            {209, "Geta Platforms"},
            {210, "Candy Platforms"},
            {211, "Light Up Platforms"},
            {212, "Rainbow Platforms"},
            {213, "Star Platforms"},
            {214, "Comfy Flip Flops"},
            {215, "Palm Flip Flops"},
            {216, "Melon Flip Flops"},
            {217, "Aqua Flip Flops"},
            {218, "Garden Flip Flops"},
            {219, "Glittery Heels"},
            {220, "Golden Heels"},
            {221, "Studded Heels"},
            {222, "Neon Heels"},
            {223, "Clear Heels"},
            {224, "Skater Sneakers"},
            {225, "Ballin' Sneakers"},
            {226, "High Top Sneakers"},
            {227, "Airy Sneakers"},
            {228, "Training Sneakers"},
            {229, "Patriotic Wedges"},
            {230, "Charcoal Wedges"},
            {231, "Cork Wedges"},
            {232, "Wooden Wedges"},
            {233, "Denim Wedges"},
            {234, "Zip Up Gladiators"},
            {235, "Strap Gladiators"},
            {236, "Weave Gladiators"},
            {237, "Modest Gladiators"},
            {238, "Web Gladiators"},
            {239, "Crafted Flats"},
            {240, "Basic Flats"},
            {241, "Floral Flats"},
            {242, "Open Flats"},
            {243, "Cozy Flats"},
            {244, "Girly Pumps"},
            {245, "Classy Pumps"},
            {246, "Shiny Pumps"},
            {247, "Polka Dot Pumps"},
            {248, "Fancy Pumps"}
        };

        //ItemType.UNIQUE_GIFT 4
        public static Dictionary<int, string> ItemsUniqueGift = new Dictionary<int, string>
        {
            {129, "Scissors"},
            {130, "Spool of Thread"},
            {131, "Buttons"},
            {132, "Pincushion"},
            {133, "Measuring Tape"},
            {134, "Gin"},
            {135, "Rum"},
            {136, "Whisky"},
            {137, "Vodka"},
            {138, "Tequila"},
            {139, "Witch Hat"},
            {140, "Jack-O'-Lantern"},
            {141, "Ouija Board"},
            {142, "Voodoo Doll"},
            {143, "Goat Skull"},
            {144, "Crystals"},
            {145, "Crystal Ball"},
            {146, "Incense"},
            {147, "Hourglass"},
            {148, "Tarot Cards"},
            {149, "Manga Book"},
            {150, "Booby Mousepad"},
            {151, "Cellphone Cover"},
            {152, "Chibi Figurine"},
            {153, "Japanese Candy"},
            {154, "Bath Salts"},
            {155, "Hot Stones"},
            {156, "Loofah Sponge"},
            {157, "Warm Towels"},
            {158, "Cucumber Slices"},
            {159, "Letter Blocks"},
            {160, "Ring Stacker"},
            {161, "Mini Xylophone"},
            {162, "Shapes Block"},
            {163, "Animal Wheel"},
            {164, "Binky"},
            {165, "Baby Cap"},
            {166, "Diapers"},
            {167, "Baby Bottle"},
            {168, "Car Carrier"},
            {169, "Clutch Purse"},
            {170, "Shoulder Bag"},
            {171, "Quilted Handbag"},
            {172, "Fancy Handbag"},
            {173, "Elegant Tote Bag"},
            {174, "Microphone"},
            {175, "Bass Guitar"},
            {176, "Drum Kit"},
            {177, "Drum Sticks"},
            {178, "Guitar Amp"},
            {179, "Spanking Paddle"},
            {180, "Ball Gag"},
            {181, "Nipple Clamps"},
            {182, "Handcuffs"},
            {183, "Chain Collar"},
            {184, "Television"},
            {185, "Jukebox"},
            {186, "Phonograph"},
            {187, "Radio"},
            {188, "Soda Machine"}
        };

        //ItemType.DATE_GIFT 5
        public static Dictionary<int, string> ItemsDateGift = new Dictionary<int, string>
        {
            {249, "Cow Plush"},
            {25, "Blue Orchid"},
            {250, "Ocean Breeze"},
            {251, "Pine Forest"},
            {252, "Pumpkin Spice"},
            {253, "Cinnamon Cider"},
            {254, "Tropical Sunset"},
            {255, "Midnight Moonlight"},
            {256, "Sweet Honeycomb"},
            {257, "Spring Rain"},
            {258, "Cotton Pillows"},
            {259, "Exfoliating Scrub"},
            {26, "Green Clover"},
            {261, "Eyelash Kit"},
            {262, "Powder Brush"},
            {263, "Makeup Palette"},
            {264, "Lipstick"},
            {265, "Moisturizer"},
            {266, "Hair Brush"},
            {268, "Blow Dryer"},
            {269, "Beach Ball"},
            {27, "Orange Daisy"},
            {270, "Inner Tube"},
            {271, "Tiki Head Charm"},
            {272, "Surfboard"},
            {273, "Snorkel Mask"},
            {274, "Flippers"},
            {275, "Suntan Lotion"},
            {276, "Beach Towel"},
            {277, "Tropical Lei"},
            {278, "Ceramic Fish"},
            {279, "Pinwheel"},
            {28, "Red Rose"},
            {280, "Hemp Bracelet"},
            {281, "Glass Dolphin"},
            {282, "Postcard"},
            {283, "Snow Globe"},
            {284, "Sanitary Pad"},
            {285, "Tampon"},
            {286, "Feminine Wash"},
            {287, "Feminine Cream"},
            {288, "Feminine Wipes"},
            {289, "Douche"},
            {29, "Pink Cosmos"},
            {291, "Sanitary Pad"},
            {292, "Tampon"},
            {293, "Douche"},
            {294, "Magic Wand"},
            {295, "Egg Vibrator"},
            {296, "Butt Plug"},
            {297, "Anal Beads"},
            {298, "Lube"},
            {299, "Dirty Dice"},
            {30, "Violet Pansy"},
            {300, "Kamasutra"},
            {301, "Breast Pump"},
            {31, "Turquoise Tulip"},
            {32, "Yellow Narcissus"},
            {33, "White Lily"},
            {34, "Sapphire Ring"},
            {35, "Emerald Bracelet"},
            {36, "Citrine Bracelet"},
            {37, "Ruby Ring"},
            {38, "Spinel Necklace"},
            {39, "Amethyst Necklace"},
            {40, "Aquamarine Earrings"},
            {41, "Topaz Earrings"},
            {42, "Diamond Tiara"},
            {43, "Peacock Plush"},
            {44, "Frog Plush"},
            {45, "Goldfish Plush"},
            {46, "Ladybug Plush"},
            {47, "Pig Plush"},
            {48, "Octopus Plush"},
            {49, "Elephant Plush"},
            {50, "Chick Plush"},
            {51, "Bunny Plush"},
            {52, "Fox Plush"}
        };

        //ItemType.BAGGAGE 6
        public static Dictionary<int, string> ItemsBaggage = new Dictionary<int, string>
        {
            {100, "Asthma"},
            {101, "Teen Angst"},
            {102, "Aquaphobic"},
            {103, "Tinnitus"},
            {104, "Kinda Crazy"},
            {105, "Annoying as Fuck"},
            {106, "Attention Whore"},
            {107, "Smelly Pussy"},
            {108, "Old Fashioned"},
            {109, "Low Self-Esteem"},
            {110, "Sheepish"},
            {111, "Intellectually Challenged"},
            {112, "Hypersensitive"},
            {113, "Forgetful"},
            {114, "Emotionally Guarded"},
            {115, "Abandonment Issues"},
            {116, "Vindictive"},
            {117, "Gold Digger"},
            {118, "Unsentimental"},
            {119, "Expensive Tastes"},
            {120, "Commitment Issues"},
            {121, "Easily Bored"},
            {122, "Allergies"},
            {123, "Self-Effacing"},
            {124, "One Pump Chump"},
            {125, "Sex Addict"},
            {126, "Jealousy"},
            {127, "Drama Queen"},
            {128, "Brand Loyalist"},
            {93, "Busy Schedule"},
            {94, "Caffeine Junkie"},
            {95, "Miss Independent"},
            {96, "Depression"},
            {97, "Emphysema"},
            {98, "Busted Vadge"},
            {99, "The Darkness"}
        };

        //ItemType.MISC 7
        public static Dictionary<int, string> ItemsMisc = new Dictionary<int, string>
        {
            {302, "Talent Level (LEVEL)"},
            {303, "Flirtation Level (LEVEL)"},
            {304, "Romance Level (LEVEL)"},
            {305, "Sexuality Level (LEVEL)"},
            {306, "Passion Level (LEVEL)"},
            {307, "Style Level (LEVEL)"},
            {308, "Talent EXP"},
            {309, "Flirtation EXP"},
            {310, "Romance EXP"},
            {311, "Sexuality EXP"},
            {312, "Suitcase of Panties"},
            {313, "Baggage Notice"},
            {314, "Ship in a Bottle"},
            {315, "Alpha Mode"},
            {316, "Non-Stop Mode"},
            {317, "Incel Difficulty"},
            {318, "Expanded Wardrobe"},
            {319, "Passion EXP"},
            {320, "Style EXP"}
        };
    }
}
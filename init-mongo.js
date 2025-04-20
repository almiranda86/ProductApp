db = db.getSiblingDB('ProductsDB');

db.createUser(
    {
        user: "root",
        pwd: "MongoDB00",
        roles: [
            {
                role: "readWrite",
                db: "ProductsDB"
            }
        ]
    }
);

db.createCollection("Products");

db.Products.insert({
    "id": "90700C24-1459-41AD-A16C-1A2756C7ADB0",
    "stock": "1",
    "description": "Test Product",
    "categories": ["Test", "Product", "App"],
    "price": "0.1"
});
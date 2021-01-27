# Billing System

This is a Billing system that manages items and shopping carts by allowing viewing and manipulating items, and generating bills.

## Functionalities

* View different Items.
* Create and Update new Items.
* Delete Items.
* Generate a bill for a shopping cart.

## Stack
* .Net Core
* Entity Framework Core
* Sqlite
* Xunit

## Run the Server
From `BillingApi` directory run:

```
dotnet run
```

## Endpoints

### GET /api/items
* General :
    * Return all available items.

* Sample: `curl -X GET http://localhost:5000/api/items`

```
[
    {
        "name": "T-Shirt",
        "price": 200,
        "discount": 10
    },
    {
        "name": "Pants",
        "price": 150,
        "discount": 5
    },
    {
        "name": "Hat",
        "price": 50,
        "discount": 0
    },
    {
        "name": "Shoes",
        "price": 100,
        "discount": 10
    },
    {
        "name": "Suit",
        "price": 1000,
        "discount": 15
    },
    {
        "name": "Jacket",
        "price": 300,
        "discount": 10
    },
    {
        "name": "Dress",
        "price": 500,
        "discount": 15
    },
    {
        "name": "SunGlasses",
        "price": 70,
        "discount": 10
    },
    {
        "name": "Jeans",
        "price": 250,
        "discount": 15
    }
]
```

### GET /api/items/{id}
* General : Return an item with the specified id.
* Sample : `curl -X GET http://localhost:5000/api/items/1`

```
{
    "name": "T-Shirt",
    "price": 200,
    "discount": 10
}
```

### POST /api/items
* General : 
    * Create a new item.
    * Returns the newly created item when successful.
* Sample : 
    * `curl -X POST http://localhost:5000/api/items`
    * Body : 
            ```
            {
                "name": "Dress",
                "price": 500,
                "discount": 10
            }
            ```

```
{
    "name": "Dress",
    "price": 500,
    "discount": 10
}
```


### PUT /api/items/{id}
* General : 
    * Update an item.
    * Return no content for successful update.
* Sample : 
    * `curl -X PUT http://localhost:5000/api/items/1`
    * Body : `{ "name": "Dress", "price": 300, "discount": 15 }`

### DELETE /api/items/{id}
* General :
    * Delete the item with the specified id.
    * Returns no content for successful delete.
* Sample : `curl -X DELETE http://localhost:5000/api/items/1`

### POST /api/cart

* General : 
    * Create a new shopping cart.
* Sample :
    * `curl http://localhost:5000/api/carts`
    * Body: 
        `
        {
            "Currency": "CAD",
            "cartItems": [
                {
                    "name": "T-Shirt",
                    "quantity": 2
                },
                {
                    "name": "Shoes",
                    "quantity": 1
                },
                {
                    "name": "Suit",
                    "quantity": 1
                }
            ]
        }
        `
        
```
{
    "billItems": [
        {
            "name": "T-Shirt",
            "price": 200,
            "discount": 10,
            "afterDiscountPrice": 180,
            "quantity": 2,
            "totalPrice": 360
        },
        {
            "name": "Shoes",
            "price": 100,
            "discount": 10,
            "afterDiscountPrice": 90,
            "quantity": 1,
            "totalPrice": 90
        },
        {
            "name": "Suit",
            "price": 1000,
            "discount": 15,
            "afterDiscountPrice": 850,
            "quantity": 1,
            "totalPrice": 850
        }
    ],
    "subTotal": 1300,
    "VAT": 14,
    "currency": "CAD",
    "total": 1482
}
```
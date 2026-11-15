# Database Schema — Bid By Bits

## Entity Relationship Overview

```
┌──────────┐     1    *   ┌─────────┐     *    1   ┌──────────┐
│   User   │──────────────▶│  Item   │──────────────│  Seller  │
│          │               │          │              │ (self-ref)│
│  - Id    │               │  - Id    │     *    1   └──────────┘
│  - Email │               │  - Title │
│  - Pass  │        1    * │  - Start │──────────▶  │ Category │
│  - Name  │──────────────▶│  - End   │              │          │
│  - Role  │   Bids        │  - Type  │     1    *   └──────────┘
│  - Score │               │  - Price │──────────▶  │   Order  │
└──────────┘               │  - Image │              │          │
         │                 │  - Owner │              │  - Id    │
         │       1    *    │  - Buyer │              │  - Item  │
         └─────────────────│  - Status│              │  - Buyer │
           Ratings        └──────────┘              │  - Seller │
                                                    │  - Paid  │
              ┌──────────────┐      1    *          │  - Shipped│
              │ Credibility  │◀─────────             │  - Refused│
              │   Log        │                       │  - Return │
              │  - Id        │                       └──────────┘
              │  - Order     │               1    *    │  Tracking│
              │  - Refused   │◀────────────            └──────────┘
              │  - Proof     │
              └──────────────┘

              ┌──────────────┐      1    *
              │   Rating     │◀─────────
              │  - Id        │
              │  - FromUser  │──▶ User (buyer)
              │  - ToUser    │──▶ User (seller)
              │  - Score     │ (1-5)
              │  - Comment   │
              └──────────────┘

              ┌──────────────┐      1    *
              │    Bid       │◀─────────
              │  - Id        │
              │  - Item      │──▶ Item
              │  - User      │──▶ User (bidder)
              │  - Amount    │
              │  - Timestamp │
              └──────────────┘
```

## Table Definitions

### Users
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| Email      | nvarchar(256)  | Unique, login                |
| PasswordHash | nvarchar(512) | Bcrypt/Argon2 hashed        |
| DisplayName | nvarchar(100) | Display name                 |
| Role       | nvarchar(20)   | 'Buyer' or 'Seller'          |
| AverageScore | decimal(3,2) | Avg credibility rating (0-5) |
| CreatedAt  | datetime2      | Registration date            |
| IsActive   | bit            | Soft-delete flag             |

### Categories
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| Name       | nvarchar(100)  | Category name                |
| ParentId   | int (FK)       | Self-ref for subcategories   |

### Items
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| Title      | nvarchar(200)  | Item title                   |
| Description| ntext          | Full description             |
| StartPrice | decimal(10,2)  | Starting bid / fixed price   |
| BestOffer  | bit            | Accept best offer flag       |
| Type       | nvarchar(20)   | 'Auction' or 'FixedPrice'    |
| ImageUrl   | nvarchar(500)  | Listing image                |
| OwnerId    | int (FK)       | → Users.Id (seller)          |
| CategoryId | int (FK)       | → Categories.Id              |
| Status     | nvarchar(20)   | 'Active','Sold','Expired','Cancelled' |
| AuctionStart | datetime2    | Auction start time           |
| AuctionEnd | datetime2      | Auction end time             |
| CreatedAt  | datetime2      | Creation timestamp           |

### Bids
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| ItemId     | int (FK)       | → Items.Id                   |
| UserId     | int (FK)       | → Users.Id (bidder)          |
| Amount     | decimal(10,2)  | Bid amount                   |
| PlacedAt   | datetime2      | When bid was placed          |

### Orders
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| ItemId     | int (FK)       | → Items.Id                   |
| BuyerId    | int (FK)       | → Users.Id (winner)          |
| SellerId   | int (FK)       | → Users.Id (owner)           |
| FinalPrice | decimal(10,2)  | Winning bid / fixed price    |
| Paid       | bit            | Payment confirmation         |
| Shipped    | bit            | Shipping confirmation        |
| TrackingNo | nvarchar(50)   | Tracking number              |
| Refused    | bit            | Buyer refused delivery       |
| ReturnProof| nvarchar(500)  | URL to proof of return delivery |

### CredibilityLogs
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| OrderId    | int (FK)       | → Orders.Id                  |
| Refused    | bit            | Delivery refused             |
| ProofGiven | bit            | Proof submitted              |
| Resolved   | bit            | Issue resolved               |
| CreatedAt  | datetime2      | Log creation time            |

### Ratings
| Column     | Type           | Notes                        |
|------------|----------------|------------------------------|
| Id         | int (PK)       | Identity                     |
| FromUserId | int (FK)       | → Users.Id (rater)           |
| ToUserId   | int (FK)       | → Users.Id (rated seller)    |
| Score      | int            | 1-5 stars                    |
| Comment    | nvarchar(500)  | Optional comment             |
| CreatedAt  | datetime2      | Rating timestamp             |

# 🌐 E-Commerces — ASP.NET Core Web API

**E-Commerces** is a modular and secure e-commerce backend built using **ASP.NET Core**, structured with **Clean Architecture**, **Repository Pattern**, **Service Layer**, and **Identity + JWT Authentication**.  
The project provides core functionalities for an online store, including product management, category filtering, and admin operations.

---

## 🚀 Features

### 🔒 Authentication & Authorization
- User registration & login  
- JWT Bearer Authentication  
- Role-based access (User / Admin)  
- Protected API routes  

### 🛒 E-Commerce Functionalities
- Product CRUD operations  
- Category management  
- Pagination & filtering  
- Admin-only management actions  
- Validation & global error handling  

### 🏗️ Architecture & Design
- Clean Architecture  
- Repository Pattern  
- Service Layer (Abstractions & Implementations)  
- Data Transfer Objects (DTOs)  
- AutoMapper configuration  
- Centralized error responses  

---

## 🛠️ Technologies Used

- **ASP.NET Core 8 Web API**  
- **Entity Framework Core**  
- **ASP.NET Identity**  
- **JWT Authentication**  
- **SQL Server**  
- **AutoMapper**  
- **Swagger (API Documentation)**  

---
## 📁 Project Structure

E-Commerces/
│
├── Domain/
│ ├── Entities/
│ ├── Models/
│ ├── Contracts/
│
├── Service/
│ ├── Abstractions/
│ ├── Implementations/
│
├── Persistence/
│ ├── Data/
│ ├── Migrations/
│ ├── Repositories/
│
├── Shared/
│ ├── DTOs/
│ ├── ErrorModels/
│
├── E-Commerces.API/
│ ├── Controllers/
│ ├── Middlewares/
│ ├── appsettings.json
│
└── E-Commerces.sln

---

## ▶️ How to Run the Project

### 1️⃣ Clone the Repository

git clone https://github.com/yasmin2624/E-Commerces.git
2️⃣ Open the Solution
Open the file:

mathematica

E-Commerces.sln
using Visual Studio 2022.

3️⃣ Configure Database
Edit appsettings.json:


"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommercesDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

4️⃣ Apply Migrations
In Package Manager Console:

Update-Database
5️⃣ Run the API
Set E-Commerces.API as the Startup Project

Click Run

Swagger UI will load automatically

🔑 Authentication Endpoints

Register

POST /api/auth/register

Login

POST /api/auth/login
Use the token in headers:


Authorization: Bearer <your_token_here>
🛒 Product Endpoints
Get all products

GET /api/products
Create product (Admin)

POST /api/products
Update product

PUT /api/products/{id}
Delete product

DELETE /api/products/{id}

📂 Category Endpoints
Get categories

GET /api/categories
Create category (Admin)

POST /api/categories
Update category

PUT /api/categories/{id}
Delete category

DELETE /api/categories/{id}

📌 Future Enhancements
Shopping Cart & Orders

Payment Integration

Admin Dashboard

Reporting & Analytics

Image Upload (Cloudinary)
❤️

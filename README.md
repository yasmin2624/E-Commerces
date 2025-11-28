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
Open E-Commerces.sln using Visual Studio 2022.

3️⃣ Configure Database
Inside appsettings.json, update:


"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommercesDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
4️⃣ Apply Migrations
Use Package Manager Console:

bash
Copy code
Update-Database
5️⃣ Run the API
Set E-Commerces.API as the startup project

Click Run

Swagger UI will open automatically

🔑 Authentication Endpoints
POST /api/auth/register
Register a new user.

POST /api/auth/login
Login and receive a JWT token.

Use the token as:

makefile
Copy code
Authorization: Bearer <token>
🛒 Product Endpoints
GET /api/products
Fetch all products.

POST /api/products (Admin)
Create a new product.

PUT /api/products/{id}
Update a product.

DELETE /api/products/{id}
Delete a product.

📂 Category Endpoints
GET /api/categories
Get all categories.

POST /api/categories (Admin)
Add a new category.

PUT /api/categories/{id}
Update category.

DELETE /api/categories/{id}
Remove category.

📌 Future Enhancements
Shopping Cart & Orders

Payment Integration

Admin Dashboard

Reporting & Analytics

Image Upload (Cloudinary)

👩‍💻 About the Developer
Yasmin Hossam Eldine Hassan
Software Developer — ASP.NET | React | Data Science
Alexandria University — Faculty of Data Science


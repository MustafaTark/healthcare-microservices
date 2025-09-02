# 🏥 Healthcare Microservices System

This repository contains a microservices-based healthcare system comprising:

- **Patient Service**: Manages patient data using **PostgreSQL** and follows the **Vertical Slice Architecture**.
- **Clinic Service**: Manages clinic data using **SQL Server** and follows the **Clean Architecture**.
- **Appointment Service**: Handles appointments between patients and clinics using **SQL Server** and **Redis** for caching.

## 🧰 Tech Stack

| Service       | Database   | Architecture             | Cache | Containerization | Orchestration |
|---------------|------------|--------------------------|-------|------------------|---------------|
| Patient       | PostgreSQL | Vertical Slice Architecture | ❌    | Docker           | Kubernetes    |
| Clinic        | SQL Server | Clean Architecture       | ❌    | Docker           | Kubernetes    |
| Appointment   | SQL Server | N/A                      | Redis | Docker           | Kubernetes    |


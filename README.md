# Cloud Sales Solution Services

## Overview

This project provides services to manage subscriptions, accounts, and software within a cloud sales solution. These services collectively handle operations such as subscription management, account management, and software provisioning with integrations to external providers.

---

## Features

### SubscriptionService
- Cancel subscriptions.
- Extend subscription validity.
- Modify subscription quantity.
- Retrieve subscriptions for a specific account.
- Provision new subscriptions.

### AccountService
- Retrieve account details.
- Manage relationships between accounts and subscriptions.

### SoftwareService
- Retrieve available software services from the CCP.
- Provision software services for customers.

---

## Project Structure

### Core Services
1. **SubscriptionService**:
   - Manages subscription lifecycle and provisioning.
2. **AccountService**:
   - Manages customer account data and associated subscriptions.
3. **SoftwareService**:
   - Interfaces with CCP to retrieve and provision software.

### Repositories
- `ISubscriptionRepository`: Handles subscription data persistence.
- `IAccountRepository`: Manages account-related data.
- `ICustomerRepository`: Manages customer-related data.

### Utilities
- `Result<T>`: A utility class for handling operation results (success/failure).
- Extension classes for domain-to-DTO and DTO-to-entity transformations.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- Dependency Injection (e.g., ASP.NET Core DI)
- Mocking library (e.g., [Moq](https://github.com/moq/moq4)) for unit testing
- Test framework (e.g., [xUnit](https://xunit.net/))

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/cloud-sales-solution.git
   cd cloud-sales-solution

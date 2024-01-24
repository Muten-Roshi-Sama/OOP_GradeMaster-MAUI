# GradeMasterMAUI

## Overview
MAUI-GradeMaster is a C# MAUI application designed to facilitate educational management tasks. It allows users to create and manage information related to students, teachers, activities, evaluations, and more. 
The application provides an intuitive interface for educational institutions to streamline their administrative processes.

## Features
- **AES Symmetric Encryption:** Unique key generated at application's FIRST initialization and stored in MAUI.SecureStorage.
- **Create Students and Teachers:** Easily add and manage student and teacher profiles.
- **Manage Activities:** Create and organize educational activities within the system.
- **Link Activities to Teachers:** Associate activities with responsible teachers for efficient organization.
- **Record Evaluations:** Keep track of student evaluations, including grades.
- **Save Data:** Utilize a reliable data-saving mechanism to preserve information for future use.
- **Generate Student Reports:** View and print student reports to assess academic performance.

## SOLID Principles
- **Single Responsibility Principle (SRP):** SRP states that a class should have only one reason to change, meaning it should have only one job or responsibility. 
  - Example: The Activity class is focused on managing the data and behavior related to a single activity. It isn't overloaded with responsibilities outside its scope, like file handling or UI logic.
- **Open/Closed Principle (OCP):** OCP suggests that software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification.
  - Example: Our usage of inheritance, where Student and Professor both derive from Person, allows for extending the functionality of Person without modifying it. This is a good example of OCP, where you can add new types of people without changing the existing Person class.
- **Liskov Substitution Principle (LSP):** LSP states that objects of a superclass shall be replaceable with objects of its subclasses without breaking the application. In other words, subclasses should not change the expected behavior of the superclass.
  - Example: The Student and Professor classes extending Person should be able to replace Person without affecting the correctness of the program. As long as methods that use Person can use Student or Professor interchangeably, LSP is being followed.
## UML Diagrams
### Class Diagram
![image](https://github.com/Muten-Roshi-Sama/OOP_GradeMaster-MAUI/assets/131618669/bb650ca5-0f26-4a61-be53-ddc528640471)



### Sequence Diagrams
![image](https://github.com/Muten-Roshi-Sama/OOP_GradeMaster-MAUI/assets/131618669/d31cbcd6-2baa-4573-b16a-0937c8c1d92c)


![image](https://github.com/Muten-Roshi-Sama/OOP_GradeMaster-MAUI/assets/131618669/eaa5be91-1759-4cc0-baf4-af9f6d4773e0)



## Getting Started

### Prerequisites
- [.NET MAUI 8.0](https://dotnet.microsoft.com/apps/maui) must be installed on your development machine.
- Make sure that the Common Language Runtime Exceptions option is not set to break when thrown. Handling error with inputs sometimes throw exceptions that interrupts the code, those are correctly handled. Your IDE should have an option to deactivate this feature. 

### Documentation
For more detailed information, check the Documentation folder. It includes:
- Class Diagram
- Sequence Diagram
- SOLID Principles Explanation

## Contributing

## License
This project is licensed under the MIT License.

## Contact
For inquiries and support, please contact me at [vassvision@protonmail.com].

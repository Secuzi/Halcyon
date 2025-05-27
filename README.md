Halcyon
A Social Media App Built with .NET MAUI

Halcyon Demo

![photo-collage png](https://github.com/user-attachments/assets/0911118c-bb87-4e7e-8f09-cbc147413344)



📌 Overview

Halcyon is a cross-platform social media application that enables users to:

Create, share, and interact with posts in real time.

Discover content through personalized feeds and search.

Build communities across geographic boundaries.

Built with .NET MAUI, it combines networking, media sharing, and communication features in a single platform.

✨ Features

✔ User Authentication – Secure login and registration.

✔ Post Management – Create, edit, and delete posts with images.

✔ Dynamic Feed – View posts from all users in a scrollable feed.

✔ Search Functionality – Find users by username or ID.

(Planned: Comments, sharing, video uploads, and messaging.)

🛠 Tech Stack
Frontend: .NET MAUI, XAML, Syncfusion MAUI Toolkit

Backend: REST API (MockAPI)

Languages: C#

Libraries:

Bogus – Mock data generation.

FFImageLoading – Optimized image handling.

ImageKit – Image upload and storage.

🚀 Installation

Prerequisites

Visual Studio 2022 (with .NET 8.0)

Android Emulator (API 33+) or physical device (Android 13+)

Setup

Clone the repository:

sh

git clone https://github.com/Secuzi/CSELEFinalProject.git

Open the solution in Visual Studio 2022.

Restore NuGet packages (automatically done in most cases).

Run the app on an Android emulator or connected device.

(Note: No additional dependencies needed—NuGet packages are pre-configured.)

📂 Project Structure
Halcyon/  
├── Context/         # User session & interaction logic  
├── Models/          # Data structures (User, Post)  
├── Services/        # API communication & data handling  
├── Utils/           # UI utilities & animations  
├── ViewModels/      # Business logic (MVVM)  
├── Views/           # UI components (XAML pages)  
└── Platforms/       # Platform-specific configurations  
🔌 API Documentation
Base URL: https://681db1d1f74de1d219b0a4f4.mockapi.io/

Endpoint	Method	Description

/user	GET	Fetch all users

/user/:id	GET	Fetch a specific user

/user	POST	Create a new user

/post	GET	Fetch all posts

/post/:id	PUT	Update a post

/post/:id	DELETE	Delete a post

/post	POST	Create a new post

(Sample requests/responses in the codebase.)

⚠ Known Issues
Some UI icons are not fully responsive on all screen sizes.

Missing features:

Comments, sharing, and bookmarks.

Password recovery.

Post recovery after deletion.

(See Future Improvements for planned fixes.)

🔮 Future Improvements
Short-term:

Enable posting without an image.

Add comment functionality.

Long-term:

Video uploads.

Direct messaging & follower system.

👥 Team
Drew Xanarie Baroro

Harold Vincent Filomeno

Russel Jarden

Faith Logarto

Regine Mae Matugas

📜 Credits
Icons: Icons8, FlatIcon

Fonts: Inter, DaFont

Mock API: MockAPI.io

📄 License
MIT (Open Source)

How to Contribute
Feel free to fork, open issues, or submit PRs!

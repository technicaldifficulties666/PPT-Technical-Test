# Project Name

This is a **.NET Full Stack Developer Technical Test** project. It is a backend API that processes a user identifier and returns an appropriate profile image URL based on specific rules.

## Features

- Fetch profile images based on a given identifier.
- Rules are applied in a specific order of precedence to determine the image URL.
- Includes a frontend interface for user interaction.

## Rules Applied

The application determines the profile image URL using the following rules in order of precedence:

1. **Last Digit is [6, 7, 8, 9]:**  
   Retrieve the image URL from `https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastDigitOfUserIdentifier}`.

2. **Last Digit is [1, 2, 3, 4, 5]:**  
   Retrieve the image URL from the SQLite database (`data.db`) where `images.id` matches the last digit.

3. **Contains Vowels (a, e, i, o, u):**  
   Display the image from `https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150`.

4. **Contains Non-Alphanumeric Characters:**  
   Display an image with a random seed between `1-5` using the URL format:  
   `https://api.dicebear.com/8.x/pixel-art/png?seed={randomNumber}&size=150`.

5. **Default Condition:**  
   If none of the above conditions are met, display the default image:  
   `https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150`.

## Setup Instructions

### Prerequisites

1. [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or higher)
2. SQLite for accessing the database (`data.db`).

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/technicaldifficulties666/PPT-Technical-Test.git
   cd PPT-Technical-Test

2. Restore project dependencies:
   ```bash
   dotnet restore

3. Start the application:
   ```bash
   dotnet run

Once the application starts, access it in your browser:
https://localhost:7171/Avatar

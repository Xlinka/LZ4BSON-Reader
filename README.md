# LZ4BSONReader

LZ4BSONReader is a C# console application designed to read `.lz4bson` files from the Resonite cache in the LocalLow folder, convert them to JSON and `.7zbson` formats.
## Features

- Recursively scans directories for `.lz4bson` files.
- Converts `.lz4bson` files to JSON and `.7zbson` formats.
- Outputs converted files to specified directories.
- Displays progress and handles exceptions.
  
## Prerequisites

- .NET 8.0 SDK or later
- Resonite game installed on your system

## Getting Started

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/LZ4BSONReader.git
    cd LZ4BSONReader
    ```

2. Ensure you have the required .NET SDK installed.

3. Restore the NuGet packages:
    ```sh
    dotnet restore
    ```

### Usage

1. Build the application:
    ```sh
    dotnet build
    ```

2. Run the application:
    ```sh
    dotnet run --project LZ4BSONReader
    ```

3. Follow the prompts to enter the input and output directory paths. The input directory should be the Resonite cache folder in the LocalLow directory.

## Project Structure

- **Program.cs**: Main entry point of the application. Handles directory input, processes files, and checks for disk space.
- **Exporter.cs**: Contains methods to export data to JSON and `.7zbson` formats.



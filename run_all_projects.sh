#!/bin/bash

# Loop through each subfolder in the current directory
for dir in */; do
    echo "Checking directory: $dir"  # Debugging line
    # Check if the subfolder contains a .csproj file
    if [ -f "$dir"*.csproj ]; then
        echo "Running C# project in $dir"
        # Run the project using dotnet with the --project option
        dotnet run --project "$dir"*.csproj
    else
        echo "No .csproj file found in $dir"  # Debugging line
    fi
done

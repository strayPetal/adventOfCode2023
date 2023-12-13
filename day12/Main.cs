public static class Main {
    const string INPUT_FILE_NAME = "puzzleInput.txt";
    
    public static int validCount = 0;

    public static void run() {
        foreach (string rowData in File.ReadLines(INPUT_FILE_NAME))
            new Row(rowData).findValid();
        Console.WriteLine($"Found {validCount} valid configurations!");
    }
}
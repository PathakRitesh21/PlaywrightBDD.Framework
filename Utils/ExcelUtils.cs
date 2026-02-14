using ClosedXML.Excel;

public static class ExcelUtils
{
    public static string GetCellData(string path, string sheet, int row, int col)
    {
        using var wb = new XLWorkbook(path);
        var ws = wb.Worksheet(sheet);
        return ws.Cell(row + 1, col + 1).GetString();
    }
}

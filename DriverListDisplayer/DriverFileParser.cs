using DriverListDisplayer.Interfaces;
using DriverListDisplayer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DriverListDisplayer
{
    public class DriverFileParser : IFileParser
    {
        //Could populate the expeted header name and type mapping from an external source. 
        private readonly Dictionary<string, ColumnInfo> _expectedDriverHeadingTypeMapping;
        private readonly FileHandler _fileHandler;

        public DriverFileParser(FileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            _expectedDriverHeadingTypeMapping = new Dictionary<string, ColumnInfo>
            {
                { RecordFields.Team, new ColumnInfo(typeof(string))},
                { RecordFields.Name, new ColumnInfo(typeof(string))},
                { RecordFields.RaceNumber, new ColumnInfo(typeof(int))},
                { RecordFields.Order, new ColumnInfo(typeof(int)) }
            };
        }


        public bool ParseDriverFile(string fileName, out List<IRecord> records)
        {
            records = new List<IRecord>();
            var (fileContents, retrievalSucceeded) = _fileHandler.GetFileContents(fileName, FileMode.Open);

            if (!retrievalSucceeded) return false;

            return IsFileContentValid(fileContents, out records);
        }


        public bool IsFileContentValid(string fileContents, out List<IRecord> records)
        {
            records = new List<IRecord>();
            if (string.IsNullOrEmpty(fileContents)) return false;
            var fileLines = fileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)?.ToList();

            if (fileLines.Count < 2) return false;

            if (!CheckHeadingsAreValid(fileLines.First())) return false;

            return CheckRecordsAreValid(fileLines.Skip(1).ToList(), records);
        }


        private bool CheckRecordsAreValid(List<string> colRecords, List<IRecord> records)
        {
            var recordsValid = false;

            recordsValid = colRecords.All(recordRow =>
               {
                   var isTypeValid = false;
                   var rowCols = recordRow.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                   if (rowCols.Length == _expectedDriverHeadingTypeMapping.Keys.Count)
                   {
                       var newRecord = new DriverRecord();
                       for (int i = 0; i < rowCols.Length; i++)
                       {
                           var colInfo = _expectedDriverHeadingTypeMapping.ElementAt(i).Value;
                           var rowColPosition = colInfo.ColPosition;
                           var rowColsType = colInfo.ColType;

                           if (rowColsType == typeof(int))
                           {
                               isTypeValid = int.TryParse(rowCols[rowColPosition], out int number);
                               if (!isTypeValid) return false;
                           }

                           newRecord.UpdateAppropriateCol(_expectedDriverHeadingTypeMapping.Keys.ElementAt(i), rowCols[rowColPosition]);

                       }
                       records.Add(newRecord);
                       return isTypeValid;
                   }
                   return false;
               });


            return recordsValid;
        }


        private bool CheckHeadingsAreValid(string colHeadings)
        {
            if (string.IsNullOrEmpty(colHeadings)) return false;
            var headings = colHeadings.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)?.ToList();

            //Ensure the expected heading amount is met
            if (headings.Count == _expectedDriverHeadingTypeMapping.Keys.Count())
            {
                for (int i = 0; i < headings.Count; i++)
                {
                    //Save the position of the column
                    if (!_expectedDriverHeadingTypeMapping.TryGetValue(headings[i].Trim(), out ColumnInfo value)) break;


                    //Save position
                    value.ColPosition = i;
                }

                return true;
            }

            return false;
        }
    }
}

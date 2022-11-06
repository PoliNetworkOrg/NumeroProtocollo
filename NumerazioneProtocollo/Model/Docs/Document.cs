﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Model.Docs
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class Document
    {
        internal static IEnumerable<VarNames.VarNames> headList = VarNames.VarNames.GenerateListDocumentsHead();
        public int? category;
        public string? filePath;
        public string? fileName;
        public DateTime? creationDate;
        public int? id;
        public int? year;

 

      

        public static object? HandleId(object? idParam, Document document)
        {
            if (idParam == null)
                return document.id;

            if (idParam is int idParamInt)
            {
                document.id = idParamInt;
                return document.id;
            }

            int? value = TryGetValueInt(idParam);
            if (value != null)
            {
                document.id = value.Value;
                return document.id;
            }
   

            return document.id;
        }


        private static int? TryGetValueInt(object? idParam)
        {
            if (idParam == null)
                return null;


            try
            {
                return Convert.ToInt32(idParam);
            }
            catch
            {
                ;
            }

            return null;
        }

        private static DateTime? TryGetValueDateTime(object? idParam)
        {
            if (idParam == null)
                return null;


            try
            {
                return Convert.ToDateTime(idParam);
            }
            catch
            {
                ;
            }

            return null;
        }

        internal static object? HandleCategoryId(object? arg1, Document document)
        {
            if (arg1 == null)
                return document.category;

            if (arg1 is int idParamInt)
            {
                document.category = idParamInt;
                return document.category;
            }

            int? value = TryGetValueInt(arg1);
            if (value != null)
            {
                document.category = value.Value;
                return document.category;
            }

            return document.category;
        }

        internal static object? HandleCategoryName(object? arg1, Document document)
        {
            return Model.Cat.Categories.GetCategoryName(document.category);
        }

        internal static object? HandleFileName(object? arg1, Document document)
        {
            if (arg1 == null)
                return document.fileName;

            if (arg1 is string idParamInt)
            {
                document.fileName = idParamInt;
                return document.fileName;
            }

            string? value = arg1.ToString();
            if (value != null)
            {
                document.fileName = value;
                return document.fileName;
            }

            return document.fileName;
        }

        internal static object? HandleCreationDate(object? arg1, Document document)
        {
            if (arg1 == null)
                return document.creationDate;

            if (arg1 is DateTime idParamInt)
            {
                document.creationDate = idParamInt;
                return document.creationDate;
            }

            DateTime? value = TryGetValueDateTime(arg1);
            if (value != null)
            {
                document.creationDate = value.Value;
                return document.creationDate;
            }

            return document.creationDate;
        }

        internal static object? HandleYear(object? arg1, Document document)
        {
            if (arg1 == null)
                return document.year;

            if (arg1 is int idParamInt)
            {
                document.year = idParamInt;
                return document.year;
            }

            int? value = TryGetValueInt(arg1);
            if (value != null)
            {
                document.year = value.Value;
                return document.year;
            }

            return document.year;
        }

        internal static Document Get(DataGridViewRow rowAdded, DataGridView dataGridView_doc)
        {
            Document document = new();
            foreach (var head in headList)
            {
                head.UpdateDocumentFromHeadAndDataRow(rowAdded, document, dataGridView_doc);
            }


            return document;
        }

   
    }
}

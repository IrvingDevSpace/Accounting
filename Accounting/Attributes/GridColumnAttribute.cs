using System;

namespace Accounting.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class GridColumnAttribute : Attribute
    {
        public Type ColumnType { get; set; } = null;
        public string ColumnName { get; set; } = "";
        public string ChildColumnName { get; set; } = "";
        public string ImagePathCompression { get; set; } = "";
        public bool Visible { get; set; } = true;
        public bool ReadOnly { get; set; } = false;

        //public GridColumnAttribute() { }

        //public GridColumnAttribute(bool readOnly, bool visible = true, Type columnType = null)
        //{
        //    ReadOnly = readOnly;
        //    Visible = visible;
        //    ColumnType = columnType;
        //}
    }
}

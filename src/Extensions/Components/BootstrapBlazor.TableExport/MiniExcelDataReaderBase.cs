#nullable disable

namespace MiniExcelLibs
{
    using System;
    using System.Data;

    /// <summary>
    /// IDataReader 实现类基类
    /// </summary>
    public abstract class MiniExcelDataReaderBase : IDataReader
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual object this[int i] => null;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual object this[string name] => null;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int Depth { get; } = 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool IsClosed { get; } = false;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int RecordsAffected { get; } = 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int FieldCount { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual bool GetBoolean(int i) => false;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual byte GetByte(int i) => byte.MinValue;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="fieldOffset"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual char GetChar(int i) => char.MinValue;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="fieldOffset"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferOffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual IDataReader GetData(int i) => null;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual string GetDataTypeName(int i) => string.Empty;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual DateTime GetDateTime(int i) => DateTime.MinValue;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual decimal GetDecimal(int i) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual double GetDouble(int i) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual Type GetFieldType(int i) => null;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual float GetFloat(int i) => 0f;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual Guid GetGuid(int i) => Guid.Empty;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual short GetInt16(int i) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual int GetInt32(int i) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual long GetInt64(int i) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual int GetOrdinal(string name) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetSchemaTable() => null;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual string GetString(int i) => string.Empty;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual int GetValues(object[] values) => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual bool IsDBNull(int i) => false;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public virtual bool NextResult() => false;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public abstract string GetName(int i);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public abstract object GetValue(int i);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public abstract bool Read();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Close()
        {

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
#nullable restore

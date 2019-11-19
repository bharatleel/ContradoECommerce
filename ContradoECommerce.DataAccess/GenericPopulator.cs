using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContradoECommerce.DataAccess
{
    public class GenericPopulator<T>
    {
        public async virtual Task<List<T>> PopulateList(SqlDataReader reader)
        {

            var results = new List<T>();
            if (Type.GetTypeCode(typeof(T)) != TypeCode.Object)
            {
                while (await reader.ReadAsync())
                {
                    results.Add((T)reader[0]);
                }
            }
            else
            {
                Func<SqlDataReader, T> readRow = this.GetReader(reader);
                while (await reader.ReadAsync())
                    results.Add(readRow(reader));
            }

            return results;
        }
        public async virtual Task<T> Populate(SqlDataReader reader)
        {
            T results = default(T);
            if (typeof(T) == typeof(string))
            {
                StringBuilder returnResult = new StringBuilder();
                while (await reader.ReadAsync())
                {
                    returnResult.Append(Convert.ToString(reader[0]));
                }
                results = (T)Convert.ChangeType(returnResult.ToString(), typeof(T));
            }
            else if (typeof(T) == typeof(bool?))
            {
                while (await reader.ReadAsync())
                {
                    if (reader[0].GetType() == typeof(DBNull))
                        results = default(T);
                    else
                        results = (T)reader[0];
                }
            }
            else if (Type.GetTypeCode(typeof(T)) != TypeCode.Object)
            {
                while (await reader.ReadAsync())
                {
                    results = (T)Convert.ChangeType(reader[0], typeof(T));
                }

            }
            else
            {
                results = (T)Activator.CreateInstance(typeof(T));
                Func<SqlDataReader, T> readRow = this.GetReader(reader);
                while (await reader.ReadAsync())
                    results = readRow(reader);
            }
            return results;
        }


        private Func<SqlDataReader, T> GetReader(SqlDataReader reader)
        {
            Delegate resDelegate;

            List<string> readerColumns = new List<string>();
            for (int index = 0; index < reader.FieldCount; index++)
                readerColumns.Add(reader.GetName(index));

            // determine the information about the reader
            var readerParam = Expression.Parameter(typeof(SqlDataReader), "reader");
            var readerGetValue = typeof(SqlDataReader).GetMethod("GetValue");

            // create a Constant expression of DBNull.Value to compare values to in reader
            //var dbNullValue = typeof(System.DBNull).GetField("Value");
            var dbNullExp = Expression.Field(expression: null, type: typeof(DBNull), fieldName: "Value");
            //Expression.Field(Expression.Parameter(typeof(System.DBNull), "System.DBNull"), dbNullValue);

            // loop through the properties and create MemberBinding expressions for each property
            List<MemberBinding> memberBindings = new List<MemberBinding>();

            foreach (var prop in typeof(T).GetProperties())
            {
                // determine the default value of the property
                object defaultValue = null;
                if (prop.PropertyType.IsValueType)
                    defaultValue = Activator.CreateInstance(prop.PropertyType);
                else if (prop.PropertyType.Name.ToLower().Equals("string"))
                    defaultValue = string.Empty;

                if (readerColumns.Contains(prop.Name))
                {
                    // build the Call expression to retrieve the data value from the reader
                    var indexExpression = Expression.Constant(reader.GetOrdinal(prop.Name));
                    var getValueExp = Expression.Call(readerParam, readerGetValue, new Expression[] { indexExpression });

                    // create the conditional expression to make sure the reader value != DBNull.Value
                    var testExp = Expression.NotEqual(dbNullExp, getValueExp);
                    var ifTrue = Expression.Convert(getValueExp, prop.PropertyType);
                    var ifFalse = Expression.Convert(Expression.Constant(defaultValue), prop.PropertyType);

                    // create the actual Bind expression to bind the value from the reader to the property value
                    MemberInfo mi = typeof(T).GetMember(prop.Name)[0];
                    MemberBinding mb = Expression.Bind(mi, Expression.Condition(testExp, ifTrue, ifFalse));
                    memberBindings.Add(mb);
                }
            }

            // create a MemberInit expression for the item with the member bindings
            var newItem = Expression.New(typeof(T));
            var memberInit = Expression.MemberInit(newItem, memberBindings);


            var lambda = Expression.Lambda<Func<SqlDataReader, T>>(memberInit, new ParameterExpression[] { readerParam });
            resDelegate = lambda.Compile();

            return (Func<SqlDataReader, T>)resDelegate;
        }
    }
}

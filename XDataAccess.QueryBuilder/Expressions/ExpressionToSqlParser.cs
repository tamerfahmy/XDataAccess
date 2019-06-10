//using System.Linq.Expressions;
//using System.Collections.Generic;
//using System.Linq;
//using System;

//namespace XDataAccess.QueryBuilder.Expressions
//{
//    internal sealed class ExpressionToSqlConverter : IExpressionConverter
//    {
//        public object ConvertParam(object param)
//        {
//            throw new NotImplementedException();
//        }

//        public object Convert(Expression exp)
//        {
//            if (exp == null) return "";

//            switch (exp.NodeType)
//            {
//                case ExpressionType.Not:
//                    var notExp = (exp as UnaryExpression)?.Operand;
//                    if (notExp.NodeType == ExpressionType.MemberAccess) return $"{Convert(notExp)} = {ConvertParam(false)}";
//                    return $"not({Convert(notExp)})";
//                case ExpressionType.Quote: return Convert((exp as UnaryExpression)?.Operand);
//                case ExpressionType.Lambda: return Convert((exp as LambdaExpression)?.Body);
//                case ExpressionType.TypeAs:
//                case ExpressionType.Convert:
//                    return Convert((exp as UnaryExpression)?.Operand);
//                case ExpressionType.Negate:
//                case ExpressionType.NegateChecked:
//                    return "-" + Convert((exp as UnaryExpression)?.Operand);
//                case ExpressionType.Constant:
//                    return ConvertParam((exp as ConstantExpression)?.Value);
//                case ExpressionType.Conditional:
//                    var condExp = exp as ConditionalExpression;
//                    return $"case when {Convert(condExp.Test)} then {Convert(condExp.IfTrue)} else {Convert(condExp.IfFalse)} end";
//                    //     case ExpressionType.Call:
//                    //         var exp3 = exp as MethodCallExpression;
//                    //         var callType = exp3.Object?.Type ?? exp3.Method.DeclaringType;
//                    //         switch (callType.FullName)
//                    //         {
//                    //             case "System.String": return ExpressionLambdaToSqlCallString(exp3, tsc);
//                    //             case "System.Math": return ExpressionLambdaToSqlCallMath(exp3, tsc);
//                    //             case "System.DateTime": return ExpressionLambdaToSqlCallDateTime(exp3, tsc);
//                    //             case "System.TimeSpan": return ExpressionLambdaToSqlCallTimeSpan(exp3, tsc);
//                    //             case "System.Convert": return ExpressionLambdaToSqlCallConvert(exp3, tsc);
//                    //         }
//                    //         if (exp3.Method.Name == "Equals" && exp3.Object != null && exp3.Arguments.Count > 0)
//                    //             return ExpressionBinary("=", exp3.Object, exp3.Arguments[0], tsc);
//                    //         if (callType.FullName.StartsWith("FreeSql.ISelectGroupingAggregate`"))
//                    //         {
//                    //             //if (exp3.Type == typeof(string) && exp3.Arguments.Any() && exp3.Arguments[0].NodeType == ExpressionType.Constant) {
//                    //             //	switch (exp3.Method.Name) {
//                    //             //		case "Sum": return $"sum({(exp3.Arguments[0] as ConstantExpression)?.Value})";
//                    //             //		case "Avg": return $"avg({(exp3.Arguments[0] as ConstantExpression)?.Value})";
//                    //             //		case "Max": return $"max({(exp3.Arguments[0] as ConstantExpression)?.Value})";
//                    //             //		case "Min": return $"min({(exp3.Arguments[0] as ConstantExpression)?.Value})";
//                    //             //	}
//                    //             //}
//                    //             switch (exp3.Method.Name)
//                    //             {
//                    //                 case "Count": return "count(1)";
//                    //                 case "Sum": return $"sum({ExpressionLambdaToSql(exp3.Arguments[0], tsc)})";
//                    //                 case "Avg": return $"avg({ExpressionLambdaToSql(exp3.Arguments[0], tsc)})";
//                    //                 case "Max": return $"max({ExpressionLambdaToSql(exp3.Arguments[0], tsc)})";
//                    //                 case "Min": return $"min({ExpressionLambdaToSql(exp3.Arguments[0], tsc)})";
//                    //             }
//                    //         }
//                    //         if (callType.FullName.StartsWith("FreeSql.ISelect`"))
//                    //         { //子表查询
//                    //             if (exp3.Method.Name == "Any")
//                    //             { //exists
//                    //                 var anyArgs = exp3.Arguments;
//                    //                 var exp3Stack = new Stack<Expression>();
//                    //                 var exp3tmp = exp3.Object;
//                    //                 if (exp3tmp != null && anyArgs.Any())
//                    //                     exp3Stack.Push(Expression.Call(exp3tmp, callType.GetMethod("Where", anyArgs.Select(a => a.Type).ToArray()), anyArgs.ToArray()));
//                    //                 while (exp3tmp != null)
//                    //                 {
//                    //                     exp3Stack.Push(exp3tmp);
//                    //                     switch (exp3tmp.NodeType)
//                    //                     {
//                    //                         case ExpressionType.Call:
//                    //                             var exp3tmpCall = (exp3tmp as MethodCallExpression);
//                    //                             exp3tmp = exp3tmpCall.Object == null ? exp3tmpCall.Arguments.FirstOrDefault() : exp3tmpCall.Object;
//                    //                             continue;
//                    //                         case ExpressionType.MemberAccess: exp3tmp = (exp3tmp as MemberExpression).Expression; continue;
//                    //                     }
//                    //                     break;
//                    //                 }
//                    //                 object fsql = null;
//                    //                 List<SelectTableInfo> fsqltables = null;
//                    //                 var fsqltable1SetAlias = false;
//                    //                 Type fsqlType = null;
//                    //                 Stack<Expression> asSelectBefores = new Stack<Expression>();
//                    //                 var asSelectSql = "";
//                    //                 Type asSelectEntityType = null;
//                    //                 MemberExpression asSelectParentExp1 = null;
//                    //                 Expression asSelectParentExp = null;
//                    //                 while (exp3Stack.Any())
//                    //                 {
//                    //                     exp3tmp = exp3Stack.Pop();
//                    //                     if (exp3tmp.Type.FullName.StartsWith("FreeSql.ISelect`") && fsql == null)
//                    //                     {
//                    //                         if (exp3tmp.NodeType == ExpressionType.Call)
//                    //                         {
//                    //                             var exp3tmpCall = (exp3tmp as MethodCallExpression);
//                    //                             if (exp3tmpCall.Method.Name == "AsSelect" && exp3tmpCall.Object == null)
//                    //                             {
//                    //                                 var exp3tmpArg1Type = exp3tmpCall.Arguments.FirstOrDefault()?.Type;
//                    //                                 if (exp3tmpArg1Type != null)
//                    //                                 {
//                    //                                     asSelectEntityType = exp3tmpArg1Type.GetElementType() ?? exp3tmpArg1Type.GenericTypeArguments.FirstOrDefault();
//                    //                                     if (asSelectEntityType != null)
//                    //                                     {
//                    //                                         fsql = _dicExpressionLambdaToSqlAsSelectMethodInfo.GetOrAdd(asSelectEntityType, asSelectEntityType2 => typeof(IFreeSql).GetMethod("Select", new Type[0]).MakeGenericMethod(asSelectEntityType2))
//                    //                                             .Invoke(_common._orm, null);

//                    //                                         if (asSelectBefores.Any())
//                    //                                         {
//                    //                                             asSelectParentExp1 = asSelectBefores.Pop() as MemberExpression;
//                    //                                             if (asSelectBefores.Any())
//                    //                                             {
//                    //                                                 asSelectParentExp = asSelectBefores.Pop();
//                    //                                                 if (asSelectParentExp != null)
//                    //                                                 {
//                    //                                                     var testExecuteExp = asSelectParentExp;
//                    //                                                     if (asSelectParentExp.NodeType == ExpressionType.Parameter) //执行leftjoin关联
//                    //                                                         testExecuteExp = Expression.Property(testExecuteExp, _common.GetTableByEntity(asSelectParentExp.Type).ColumnsByCs.First().Key);
//                    //                                                     var tsc2 = tsc.CloneSetgetSelectGroupingMapStringAndgetSelectGroupingMapStringAndtbtype(new List<SelectColumnInfo>(), tsc.getSelectGroupingMapString, SelectTableInfoType.LeftJoin);
//                    //                                                     tsc2.isDisableDiyParse = true;
//                    //                                                     tsc2.style = ExpressionStyle.AsSelect;
//                    //                                                     asSelectSql = ExpressionLambdaToSql(testExecuteExp, tsc2);
//                    //                                                 }
//                    //                                             }
//                    //                                         }
//                    //                                     }
//                    //                                 }
//                    //                             }
//                    //                         }
//                    //                         if (fsql == null) fsql = Expression.Lambda(exp3tmp).Compile().DynamicInvoke();
//                    //                         fsqlType = fsql?.GetType();
//                    //                         if (fsqlType == null) break;
//                    //                         fsqlType.GetField("_limit", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(fsql, 1);
//                    //                         fsqltables = fsqlType.GetField("_tables", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(fsql) as List<SelectTableInfo>;
//                    //                         //fsqltables[0].Alias = $"{tsc._tables[0].Alias}_{fsqltables[0].Alias}";
//                    //                         fsqltables.AddRange(tsc._tables.Select(a => new SelectTableInfo
//                    //                         {
//                    //                             Alias = a.Alias,
//                    //                             On = "1=1",
//                    //                             Table = a.Table,
//                    //                             Type = SelectTableInfoType.Parent,
//                    //                             Parameter = a.Parameter
//                    //                         }));
//                    //                     }
//                    //                     else if (fsqlType != null)
//                    //                     {
//                    //                         var call3Exp = exp3tmp as MethodCallExpression;
//                    //                         var method = fsqlType.GetMethod(call3Exp.Method.Name, call3Exp.Arguments.Select(a => a.Type).ToArray());
//                    //                         if (call3Exp.Method.ContainsGenericParameters) method.MakeGenericMethod(call3Exp.Method.GetGenericArguments());
//                    //                         var parms = method.GetParameters();
//                    //                         var args = new object[call3Exp.Arguments.Count];
//                    //                         for (var a = 0; a < args.Length; a++)
//                    //                         {
//                    //                             var arg3Exp = call3Exp.Arguments[a];
//                    //                             if (arg3Exp.NodeType == ExpressionType.Constant)
//                    //                             {
//                    //                                 args[a] = (arg3Exp as ConstantExpression)?.Value;
//                    //                             }
//                    //                             else
//                    //                             {
//                    //                                 var argExp = (arg3Exp as UnaryExpression)?.Operand;
//                    //                                 if (argExp != null && argExp.NodeType == ExpressionType.Lambda)
//                    //                                 {
//                    //                                     if (fsqltable1SetAlias == false)
//                    //                                     {
//                    //                                         fsqltables[0].Alias = (argExp as LambdaExpression).Parameters.First().Name;
//                    //                                         fsqltable1SetAlias = true;
//                    //                                     }
//                    //                                 }
//                    //                                 args[a] = argExp ?? Expression.Lambda(arg3Exp).Compile().DynamicInvoke();
//                    //                                 //if (args[a] == null) ExpressionLambdaToSql(call3Exp.Arguments[a], fsqltables, null, null, SelectTableInfoType.From, true);
//                    //                             }
//                    //                         }
//                    //                         method.Invoke(fsql, args);
//                    //                     }
//                    //                     if (fsql == null) asSelectBefores.Push(exp3tmp);
//                    //                 }
//                    //                 if (fsql != null)
//                    //                 {
//                    //                     if (asSelectParentExp != null)
//                    //                     { //执行 asSelect() 的关联，OneToMany，ManyToMany
//                    //                         if (fsqltables[0].Parameter == null)
//                    //                         {
//                    //                             fsqltables[0].Alias = $"tb_{fsqltables.Count}";
//                    //                             fsqltables[0].Parameter = Expression.Parameter(asSelectEntityType, fsqltables[0].Alias);
//                    //                         }
//                    //                         var fsqlWhere = _dicExpressionLambdaToSqlAsSelectWhereMethodInfo.GetOrAdd(asSelectEntityType, asSelectEntityType3 =>
//                    //                             typeof(ISelect<>).MakeGenericType(asSelectEntityType3).GetMethod("Where", new[] {
//                    //                                 typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(asSelectEntityType3, typeof(bool)))
//                    //                         }));
//                    //                         var parm123Tb = _common.GetTableByEntity(asSelectParentExp.Type);
//                    //                         var parm123Ref = parm123Tb.GetTableRef(asSelectParentExp1.Member.Name, true);
//                    //                         var fsqlWhereParam = fsqltables.First().Parameter; //Expression.Parameter(asSelectEntityType);
//                    //                         Expression fsqlWhereExp = null;
//                    //                         if (parm123Ref.RefType == TableRefType.ManyToMany)
//                    //                         {
//                    //                             //g.mysql.Select<Tag>().Where(a => g.mysql.Select<Song_tag>().Where(b => b.Tag_id == a.Id && b.Song_id == 1).Any());
//                    //                             var manyTb = _common.GetTableByEntity(parm123Ref.RefMiddleEntityType);
//                    //                             var manySubSelectWhere = _dicExpressionLambdaToSqlAsSelectWhereMethodInfo.GetOrAdd(parm123Ref.RefMiddleEntityType, refMiddleEntityType3 =>
//                    //                                 typeof(ISelect<>).MakeGenericType(refMiddleEntityType3).GetMethod("Where", new[] {
//                    //                                 typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(refMiddleEntityType3, typeof(bool)))
//                    //                             }));
//                    //                             var manySubSelectWhereSql = _dicExpressionLambdaToSqlAsSelectWhereSqlMethodInfo.GetOrAdd(parm123Ref.RefMiddleEntityType, refMiddleEntityType3 =>
//                    //                                 typeof(ISelect0<,>).MakeGenericType(typeof(ISelect<>).MakeGenericType(refMiddleEntityType3), refMiddleEntityType3).GetMethod("Where", new[] { typeof(string), typeof(object) }));
//                    //                             var manySubSelectAny = _dicExpressionLambdaToSqlAsSelectAnyMethodInfo.GetOrAdd(parm123Ref.RefMiddleEntityType, refMiddleEntityType3 =>
//                    //                                 typeof(ISelect0<,>).MakeGenericType(typeof(ISelect<>).MakeGenericType(refMiddleEntityType3), refMiddleEntityType3).GetMethod("Any", new Type[0]));
//                    //                             var manySubSelectAsSelectExp = _dicFreeSqlGlobalExtensionsAsSelectExpression.GetOrAdd(parm123Ref.RefMiddleEntityType, refMiddleEntityType3 =>
//                    //                                 Expression.Call(
//                    //                                     typeof(FreeSqlGlobalExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(mfil => mfil.Name == "AsSelect" && mfil.GetParameters().Length == 1).FirstOrDefault()?.MakeGenericMethod(refMiddleEntityType3),
//                    //                                     Expression.Constant(Activator.CreateInstance(typeof(List<>).MakeGenericType(refMiddleEntityType3)))
//                    //                                 ));
//                    //                             var manyMainParam = tsc._tables[0].Parameter;
//                    //                             var manySubSelectWhereParam = Expression.Parameter(parm123Ref.RefMiddleEntityType, $"M{fsqlWhereParam.Name}_M{asSelectParentExp.ToString().Replace(".", "__")}");//, $"{fsqlWhereParam.Name}__");
//                    //                             Expression manySubSelectWhereExp = null;
//                    //                             for (var mn = 0; mn < parm123Ref.Columns.Count; mn++)
//                    //                             {
//                    //                                 var col1 = parm123Ref.MiddleColumns[mn];
//                    //                                 var col2 = parm123Ref.Columns[mn];
//                    //                                 var pexp1 = Expression.Property(manySubSelectWhereParam, col1.CsName);
//                    //                                 var pexp2 = Expression.Property(asSelectParentExp, col2.CsName);
//                    //                                 if (col1.CsType != col2.CsType)
//                    //                                 {
//                    //                                     if (col1.CsType.IsNullableType()) pexp1 = Expression.Property(pexp1, _dicNullableValueProperty.GetOrAdd(col1.CsType, ct1 => ct1.GetProperty("Value")));
//                    //                                     if (col2.CsType.IsNullableType()) pexp2 = Expression.Property(pexp2, _dicNullableValueProperty.GetOrAdd(col2.CsType, ct2 => ct2.GetProperty("Value")));
//                    //                                 }
//                    //                                 var tmpExp = Expression.Equal(pexp1, pexp2);
//                    //                                 if (mn == 0) manySubSelectWhereExp = tmpExp;
//                    //                                 else manySubSelectWhereExp = Expression.AndAlso(manySubSelectWhereExp, tmpExp);
//                    //                             }
//                    //                             var manySubSelectExpBoy = Expression.Call(
//                    //                                 manySubSelectAsSelectExp,
//                    //                                 manySubSelectWhere,
//                    //                                 Expression.Lambda(
//                    //                                     manySubSelectWhereExp,
//                    //                                     manySubSelectWhereParam
//                    //                                 )
//                    //                             );
//                    //                             Expression fsqlManyWhereExp = null;
//                    //                             for (var mn = 0; mn < parm123Ref.RefColumns.Count; mn++)
//                    //                             {
//                    //                                 var col1 = parm123Ref.RefColumns[mn];
//                    //                                 var col2 = parm123Ref.MiddleColumns[mn + parm123Ref.Columns.Count + mn];
//                    //                                 var pexp1 = Expression.Property(fsqlWhereParam, col1.CsName);
//                    //                                 var pexp2 = Expression.Property(manySubSelectWhereParam, col2.CsName);
//                    //                                 if (col1.CsType != col2.CsType)
//                    //                                 {
//                    //                                     if (col1.CsType.IsNullableType()) pexp1 = Expression.Property(pexp1, _dicNullableValueProperty.GetOrAdd(col1.CsType, ct1 => ct1.GetProperty("Value")));
//                    //                                     if (col2.CsType.IsNullableType()) pexp2 = Expression.Property(pexp2, _dicNullableValueProperty.GetOrAdd(col2.CsType, ct2 => ct2.GetProperty("Value")));
//                    //                                 }
//                    //                                 var tmpExp = Expression.Equal(pexp1, pexp2);
//                    //                                 if (mn == 0) fsqlManyWhereExp = tmpExp;
//                    //                                 else fsqlManyWhereExp = Expression.AndAlso(fsqlManyWhereExp, tmpExp);
//                    //                             }
//                    //                             fsqltables.Add(new SelectTableInfo { Alias = manySubSelectWhereParam.Name, Parameter = manySubSelectWhereParam, Table = manyTb, Type = SelectTableInfoType.Parent });
//                    //                             fsqlWhere.Invoke(fsql, new object[] { Expression.Lambda(fsqlManyWhereExp, fsqlWhereParam) });
//                    //                             var sql2 = fsqlType.GetMethod("ToSql", new Type[] { typeof(string) })?.Invoke(fsql, new object[] { "1" })?.ToString();
//                    //                             if (string.IsNullOrEmpty(sql2) == false)
//                    //                                 manySubSelectExpBoy = Expression.Call(manySubSelectExpBoy, manySubSelectWhereSql, Expression.Constant($"exists({sql2.Replace("\r\n", "\r\n\t")})"), Expression.Constant(null));
//                    //                             manySubSelectExpBoy = Expression.Call(manySubSelectExpBoy, manySubSelectAny);
//                    //                             asSelectBefores.Clear();

//                    //                             return ExpressionLambdaToSql(manySubSelectExpBoy, tsc);
//                    //                         }
//                    //                         for (var mn = 0; mn < parm123Ref.Columns.Count; mn++)
//                    //                         {
//                    //                             var col1 = parm123Ref.RefColumns[mn];
//                    //                             var col2 = parm123Ref.Columns[mn];
//                    //                             var pexp1 = Expression.Property(fsqlWhereParam, col1.CsName);
//                    //                             var pexp2 = Expression.Property(asSelectParentExp, col2.CsName);
//                    //                             if (col1.CsType != col2.CsType)
//                    //                             {
//                    //                                 if (col1.CsType.IsNullableType()) pexp1 = Expression.Property(pexp1, _dicNullableValueProperty.GetOrAdd(col1.CsType, ct1 => ct1.GetProperty("Value")));
//                    //                                 if (col2.CsType.IsNullableType()) pexp2 = Expression.Property(pexp2, _dicNullableValueProperty.GetOrAdd(col2.CsType, ct2 => ct2.GetProperty("Value")));
//                    //                             }
//                    //                             var tmpExp = Expression.Equal(pexp1, pexp2);
//                    //                             if (mn == 0) fsqlWhereExp = tmpExp;
//                    //                             else fsqlWhereExp = Expression.AndAlso(fsqlWhereExp, tmpExp);
//                    //                         }
//                    //                         fsqlWhere.Invoke(fsql, new object[] { Expression.Lambda(fsqlWhereExp, fsqlWhereParam) });
//                    //                     }
//                    //                     asSelectBefores.Clear();
//                    //                     var sql = fsqlType.GetMethod("ToSql", new Type[] { typeof(string) })?.Invoke(fsql, new object[] { "1" })?.ToString();
//                    //                     if (string.IsNullOrEmpty(sql) == false)
//                    //                         return $"exists({sql.Replace("\r\n", "\r\n\t")})";
//                    //                 }
//                    //                 asSelectBefores.Clear();
//                    //             }
//                    //         }
//                    //         //var eleType = callType.GetElementType() ?? callType.GenericTypeArguments.FirstOrDefault();
//                    //         //if (eleType != null && typeof(IEnumerable<>).MakeGenericType(eleType).IsAssignableFrom(callType)) { //集合导航属性子查询
//                    //         //	if (exp3.Method.Name == "Any") { //exists

//                    //         //	}
//                    //         //}
//                    //         var other3Exp = ExpressionLambdaToSqlOther(exp3, tsc);
//                    //         if (string.IsNullOrEmpty(other3Exp) == false) return other3Exp;
//                    //         throw new Exception($"未实现函数表达式 {exp3} 解析");
//                    //     case ExpressionType.Parameter:
//                    //     case ExpressionType.MemberAccess:
//                    //         var exp4 = exp as MemberExpression;
//                    //         if (exp4 != null)
//                    //         {
//                    //             if (exp4.Expression != null && exp4.Expression.Type.IsArray == false && exp4.Expression.Type.IsNullableType()) return ExpressionLambdaToSql(exp4.Expression, tsc);
//                    //             var extRet = "";
//                    //             var memberType = exp4.Expression?.Type ?? exp4.Type;
//                    //             switch (memberType.FullName)
//                    //             {
//                    //                 case "System.String": extRet = ExpressionLambdaToSqlMemberAccessString(exp4, tsc); break;
//                    //                 case "System.DateTime": extRet = ExpressionLambdaToSqlMemberAccessDateTime(exp4, tsc); break;
//                    //                 case "System.TimeSpan": extRet = ExpressionLambdaToSqlMemberAccessTimeSpan(exp4, tsc); break;
//                    //             }
//                    //             if (string.IsNullOrEmpty(extRet) == false) return extRet;
//                    //             var other4Exp = ExpressionLambdaToSqlOther(exp4, tsc);
//                    //             if (string.IsNullOrEmpty(other4Exp) == false) return other4Exp;
//                    //         }
//                    //         var expStack = new Stack<Expression>();
//                    //         expStack.Push(exp);
//                    //         MethodCallExpression callExp = null;
//                    //         var exp2 = exp4?.Expression;
//                    //         while (true)
//                    //         {
//                    //             switch (exp2?.NodeType)
//                    //             {
//                    //                 case ExpressionType.Constant:
//                    //                     expStack.Push(exp2);
//                    //                     break;
//                    //                 case ExpressionType.Parameter:
//                    //                     expStack.Push(exp2);
//                    //                     break;
//                    //                 case ExpressionType.MemberAccess:
//                    //                     expStack.Push(exp2);
//                    //                     exp2 = (exp2 as MemberExpression).Expression;
//                    //                     if (exp2 == null) break;
//                    //                     continue;
//                    //                 case ExpressionType.Call:
//                    //                     callExp = exp2 as MethodCallExpression;
//                    //                     expStack.Push(exp2);
//                    //                     exp2 = callExp.Object;
//                    //                     if (exp2 == null) break;
//                    //                     continue;
//                    //                 case ExpressionType.TypeAs:
//                    //                 case ExpressionType.Convert:
//                    //                     var oper2 = (exp2 as UnaryExpression).Operand;
//                    //                     if (oper2.NodeType == ExpressionType.Parameter)
//                    //                     {
//                    //                         var oper2Parm = oper2 as ParameterExpression;
//                    //                         expStack.Push(Expression.Parameter(exp2.Type, oper2Parm.Name));
//                    //                     }
//                    //                     else
//                    //                         expStack.Push(oper2);
//                    //                     break;
//                    //             }
//                    //             break;
//                    //         }
//                    //         if (expStack.First().NodeType != ExpressionType.Parameter) return formatSql(Expression.Lambda(exp).Compile().DynamicInvoke(), tsc.mapType);
//                    //         if (callExp != null) return ExpressionLambdaToSql(callExp, tsc);
//                    //         if (tsc.getSelectGroupingMapString != null && expStack.First().Type.FullName.StartsWith("FreeSql.ISelectGroupingAggregate`"))
//                    //         {
//                    //             if (tsc.getSelectGroupingMapString != null)
//                    //             {
//                    //                 var expText = tsc.getSelectGroupingMapString(expStack.Where((a, b) => b >= 2).ToArray());
//                    //                 if (string.IsNullOrEmpty(expText) == false) return expText;
//                    //             }
//                    //         }

//                    //         if (tsc._tables == null)
//                    //         {
//                    //             var pp = expStack.Pop() as ParameterExpression;
//                    //             var memberExp = expStack.Pop() as MemberExpression;
//                    //             var tb = _common.GetTableByEntity(pp.Type);
//                    //             if (tb.ColumnsByCs.ContainsKey(memberExp.Member.Name) == false) throw new ArgumentException($"{tb.DbName} 找不到列 {memberExp.Member.Name}");
//                    //             if (tsc._selectColumnMap != null)
//                    //             {
//                    //                 tsc._selectColumnMap.Add(new SelectColumnInfo { Table = null, Column = tb.ColumnsByCs[memberExp.Member.Name] });
//                    //             }
//                    //             var name = tb.ColumnsByCs[memberExp.Member.Name].Attribute.Name;
//                    //             if (tsc.isQuoteName) name = _common.QuoteSqlName(name);
//                    //             return name;
//                    //         }
//                    //         Func<TableInfo, string, bool, ParameterExpression, MemberExpression, SelectTableInfo> getOrAddTable = (tbtmp, alias, isa, parmExp, mp) =>
//                    //         {
//                    //             var finds = new SelectTableInfo[0];
//                    //             if (tsc.style == ExpressionStyle.SelectColumns)
//                    //             {
//                    //                 finds = tsc._tables.Where(a => a.Table.Type == tbtmp.Type).ToArray();
//                    //                 if (finds.Any()) finds = new[] { finds.First() };
//                    //             }
//                    //             if (finds.Length != 1 && isa && parmExp != null)
//                    //                 finds = tsc._tables.Where(a => a.Parameter == parmExp).ToArray();
//                    //             if (finds.Length != 1)
//                    //             {
//                    //                 var navdot = string.IsNullOrEmpty(alias) ? new SelectTableInfo[0] : tsc._tables.Where(a2 => a2.Parameter != null && alias.StartsWith($"{a2.Alias}__")).ToArray();
//                    //                 if (navdot.Length > 0)
//                    //                 {
//                    //                     var isthis = navdot[0] == tsc._tables[0];
//                    //                     finds = tsc._tables.Where(a2 => (isa && a2.Parameter != null || !isa && a2.Parameter == null) &&
//                    //                         a2.Table.Type == tbtmp.Type && a2.Alias == alias && a2.Alias.StartsWith($"{navdot[0].Alias}__") &&
//                    //                         (isthis && a2.Type != SelectTableInfoType.Parent || !isthis && a2.Type == SelectTableInfoType.Parent)).ToArray();
//                    //                     if (finds.Length == 0)
//                    //                         finds = tsc._tables.Where(a2 =>
//                    //                              a2.Table.Type == tbtmp.Type && a2.Alias == alias && a2.Alias.StartsWith($"{navdot[0].Alias}__") &&
//                    //                              (isthis && a2.Type != SelectTableInfoType.Parent || !isthis && a2.Type == SelectTableInfoType.Parent)).ToArray();
//                    //                 }
//                    //                 else
//                    //                 {
//                    //                     finds = tsc._tables.Where(a2 => (isa && a2.Parameter != null || isa && a2.Parameter == null) &&
//                    //                         a2.Table.Type == tbtmp.Type && a2.Alias == alias).ToArray();
//                    //                     if (finds.Length != 1)
//                    //                     {
//                    //                         finds = tsc._tables.Where(a2 => (isa && a2.Parameter != null || isa && a2.Parameter == null) &&
//                    //                             a2.Table.Type == tbtmp.Type).ToArray();
//                    //                         if (finds.Length != 1)
//                    //                         {
//                    //                             finds = tsc._tables.Where(a2 => (isa && a2.Parameter != null || isa && a2.Parameter == null) &&
//                    //                                 a2.Table.Type == tbtmp.Type).ToArray();
//                    //                             if (finds.Length != 1)
//                    //                                 finds = tsc._tables.Where(a2 => a2.Table.Type == tbtmp.Type).ToArray();
//                    //                         }
//                    //                     }
//                    //                 }
//                    //                 //finds = tsc._tables.Where((a2, c2) => (isa || a2.Parameter == null) && a2.Table.CsName == tbtmp.CsName && (isthis && a2.Type != SelectTableInfoType.Parent || !isthis)).ToArray(); //外部表，内部表一起查
//                    //                 //if (finds.Length > 1) {
//                    //                 //	finds = tsc._tables.Where((a2, c2) => (isa || a2.Parameter == null) && a2.Table.CsName == tbtmp.CsName && a2.Type == SelectTableInfoType.Parent && a2.Alias == alias).ToArray(); //查询外部表
//                    //                 //	if (finds.Any() == false) {
//                    //                 //		finds = tsc._tables.Where((a2, c2) => (isa || a2.Parameter == null) && a2.Table.CsName == tbtmp.CsName && a2.Type != SelectTableInfoType.Parent).ToArray(); //查询内部表
//                    //                 //		if (finds.Length > 1)
//                    //                 //			finds = tsc._tables.Where((a2, c2) => (isa || a2.Parameter == null) && a2.Table.CsName == tbtmp.CsName && a2.Type != SelectTableInfoType.Parent && a2.Alias == alias).ToArray();
//                    //                 //	}
//                    //                 //}
//                    //             }
//                    //             var find = finds.Length == 1 ? finds.First() : null;
//                    //             if (find != null && isa && parmExp != null && find.Parameter != parmExp)
//                    //                 find.Parameter = parmExp;
//                    //             if (find == null)
//                    //             {
//                    //                 tsc._tables.Add(find = new SelectTableInfo { Table = tbtmp, Alias = alias, On = null, Type = mp == null ? tsc.tbtype : SelectTableInfoType.LeftJoin, Parameter = isa ? parmExp : null });
//                    //                 if (mp?.Expression != null)
//                    //                 { //导航条件，OneToOne、ManyToOne
//                    //                     var firstTb = tsc._tables.First().Table;
//                    //                     var parentTb = _common.GetTableByEntity(mp.Expression.Type);
//                    //                     var parentTbRef = parentTb?.GetTableRef(mp.Member.Name, tsc.style == ExpressionStyle.AsSelect);
//                    //                     if (parentTbRef != null)
//                    //                     {
//                    //                         Expression navCondExp = null;
//                    //                         for (var mn = 0; mn < parentTbRef.Columns.Count; mn++)
//                    //                         {
//                    //                             var col1 = parentTbRef.RefColumns[mn];
//                    //                             var col2 = parentTbRef.Columns[mn];
//                    //                             var pexp1 = Expression.Property(mp, col1.CsName);
//                    //                             var pexp2 = Expression.Property(mp.Expression, col2.CsName);
//                    //                             if (col1.CsType != col2.CsType)
//                    //                             {
//                    //                                 if (col1.CsType.IsNullableType()) pexp1 = Expression.Property(pexp1, _dicNullableValueProperty.GetOrAdd(col1.CsType, ct1 => ct1.GetProperty("Value")));
//                    //                                 if (col2.CsType.IsNullableType()) pexp2 = Expression.Property(pexp2, _dicNullableValueProperty.GetOrAdd(col2.CsType, ct2 => ct2.GetProperty("Value")));
//                    //                             }
//                    //                             var tmpExp = Expression.Equal(pexp1, pexp2);
//                    //                             if (mn == 0) navCondExp = tmpExp;
//                    //                             else navCondExp = Expression.AndAlso(navCondExp, tmpExp);
//                    //                         }
//                    //                         if (find.Type == SelectTableInfoType.InnerJoin ||
//                    //                             find.Type == SelectTableInfoType.LeftJoin ||
//                    //                             find.Type == SelectTableInfoType.RightJoin)
//                    //                             find.On = ExpressionLambdaToSql(navCondExp, tsc.CloneSetgetSelectGroupingMapStringAndgetSelectGroupingMapStringAndtbtype(null, null, find.Type));
//                    //                         else
//                    //                             find.NavigateCondition = ExpressionLambdaToSql(navCondExp, tsc.CloneSetgetSelectGroupingMapStringAndgetSelectGroupingMapStringAndtbtype(null, null, find.Type));
//                    //                     }
//                    //                 }
//                    //             }
//                    //             return find;
//                    //         };

//                    //         TableInfo tb2 = null;
//                    //         ParameterExpression parmExp2 = null;
//                    //         string alias2 = "", name2 = "";
//                    //         SelectTableInfo find2 = null;
//                    //         while (expStack.Count > 0)
//                    //         {
//                    //             exp2 = expStack.Pop();
//                    //             switch (exp2.NodeType)
//                    //             {
//                    //                 case ExpressionType.Constant:
//                    //                     throw new NotImplementedException("未实现 MemberAccess 下的 Constant");
//                    //                 case ExpressionType.Parameter:
//                    //                 case ExpressionType.MemberAccess:

//                    //                     var exp2Type = exp2.Type;
//                    //                     if (exp2Type.FullName.StartsWith("FreeSql.ISelectGroupingAggregate`")) exp2Type = exp2Type.GenericTypeArguments.LastOrDefault() ?? exp2.Type;
//                    //                     var tb2tmp = _common.GetTableByEntity(exp2Type);
//                    //                     var mp2 = exp2 as MemberExpression;
//                    //                     if (mp2?.Member.Name == "Key" && mp2.Expression.Type.FullName.StartsWith("FreeSql.ISelectGroupingAggregate`")) continue;
//                    //                     if (tb2tmp != null)
//                    //                     {
//                    //                         if (exp2.NodeType == ExpressionType.Parameter)
//                    //                         {
//                    //                             parmExp2 = (exp2 as ParameterExpression);
//                    //                             alias2 = parmExp2.Name;
//                    //                         }
//                    //                         else alias2 = $"{alias2}__{mp2.Member.Name}";
//                    //                         find2 = getOrAddTable(tb2tmp, alias2, exp2.NodeType == ExpressionType.Parameter, parmExp2, mp2);
//                    //                         alias2 = find2.Alias;
//                    //                         tb2 = tb2tmp;
//                    //                     }
//                    //                     if (exp2.NodeType == ExpressionType.Parameter && expStack.Any() == false)
//                    //                     { //附加选择的参数所有列
//                    //                         if (tsc._selectColumnMap != null)
//                    //                         {
//                    //                             foreach (var tb2c in tb2.Columns.Values)
//                    //                                 tsc._selectColumnMap.Add(new SelectColumnInfo { Table = find2, Column = tb2c });
//                    //                             if (tb2.Columns.Any()) return "";
//                    //                         }
//                    //                     }
//                    //                     if (mp2 == null || expStack.Any()) continue;
//                    //                     if (tb2.ColumnsByCs.ContainsKey(mp2.Member.Name) == false)
//                    //                     { //如果选的是对象，附加所有列
//                    //                         if (tsc._selectColumnMap != null)
//                    //                         {
//                    //                             var tb3 = _common.GetTableByEntity(mp2.Type);
//                    //                             if (tb3 != null)
//                    //                             {
//                    //                                 var find3 = getOrAddTable(tb2tmp, alias2 /*$"{alias2}__{mp2.Member.Name}"*/, exp2.NodeType == ExpressionType.Parameter, parmExp2, mp2);

//                    //                                 foreach (var tb3c in tb3.Columns.Values)
//                    //                                     tsc._selectColumnMap.Add(new SelectColumnInfo { Table = find3, Column = tb3c });
//                    //                                 if (tb3.Columns.Any()) return "";
//                    //                             }
//                    //                         }
//                    //                         throw new ArgumentException($"{tb2.DbName} 找不到列 {mp2.Member.Name}");
//                    //                     }
//                    //                     var col2 = tb2.ColumnsByCs[mp2.Member.Name];
//                    //                     if (tsc._selectColumnMap != null && find2 != null)
//                    //                     {
//                    //                         tsc._selectColumnMap.Add(new SelectColumnInfo { Table = find2, Column = col2 });
//                    //                         return "";
//                    //                     }
//                    //                     name2 = tb2.ColumnsByCs[mp2.Member.Name].Attribute.Name;
//                    //                     break;
//                    //                 case ExpressionType.Call: break;
//                    //             }
//                    //         }
//                    //         if (tsc.isQuoteName) name2 = _common.QuoteSqlName(name2);
//                    //         return $"{alias2}.{name2}";


//            }
//            var expBinary = exp as BinaryExpression;
//            if (expBinary == null)
//            {
//                //     var other99Exp = ExpressionLambdaToSqlOther(exp, tsc);
//                //     if (string.IsNullOrEmpty(other99Exp) == false) return other99Exp;
//                //     return "";
//            }
//            switch (expBinary.NodeType)
//            {
//                case ExpressionType.Coalesce:
//                    throw new NotImplementedException();
//                    //         return _common.IsNull(ExpressionLambdaToSql(expBinary.Left, tsc), ExpressionLambdaToSql(expBinary.Right, tsc));
//            }

//            // if (dicExpressionOperator.TryGetValue(expBinary.NodeType, out var tryoper) == false) return "";
//            // return ExpressionBinary(tryoper, expBinary.Left, expBinary.Right, tsc);

//            throw new NotImplementedException();
//        }
//    }
//}
﻿namespace System.Data.Entity.Core.Common.EntitySql
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Core.Common.CommandTrees;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Entity SQL Parser result information.
    /// </summary>
    public sealed class ParseResult
    {
        private readonly DbCommandTree _commandTree;
        private readonly ReadOnlyCollection<FunctionDefinition> _functionDefs;

        internal ParseResult(DbCommandTree commandTree, List<FunctionDefinition> functionDefs)
        {
            Contract.Requires(commandTree != null);
            Contract.Requires(functionDefs != null);

            _commandTree = commandTree;
            _functionDefs = functionDefs.AsReadOnly();
        }

        /// <summary>
        /// A command tree produced during parsing.
        /// </summary>
        public DbCommandTree CommandTree
        {
            get { return _commandTree; }
        }

        /// <summary>
        /// List of <see cref="FunctionDefinition"/> objects describing query inline function definitions.
        /// </summary>
        public ReadOnlyCollection<FunctionDefinition> FunctionDefinitions
        {
            get { return _functionDefs; }
        }
    }

    /// <summary>
    /// Entity SQL query inline function definition, returned as a part of <see cref="ParseResult"/>.
    /// </summary>
    public sealed class FunctionDefinition
    {
        private readonly string _name;
        private readonly DbLambda _lambda;
        private readonly int _startPosition;
        private readonly int _endPosition;

        internal FunctionDefinition(string name, DbLambda lambda, int startPosition, int endPosition)
        {
            Debug.Assert(name != null, "name can not be null");
            Debug.Assert(lambda != null, "lambda cannot be null");

            _name = name;
            _lambda = lambda;
            _startPosition = startPosition;
            _endPosition = endPosition;
        }

        /// <summary>
        /// Function name.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Function body and parameters.
        /// </summary>
        public DbLambda Lambda
        {
            get { return _lambda; }
        }

        /// <summary>
        /// Start position of the function definition in the eSQL query text.
        /// </summary>
        public int StartPosition
        {
            get { return _startPosition; }
        }

        /// <summary>
        /// End position of the function definition in the eSQL query text.
        /// </summary>
        public int EndPosition
        {
            get { return _endPosition; }
        }
    }
}
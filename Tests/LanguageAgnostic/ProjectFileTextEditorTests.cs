﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.CodeConverter.CSharp;
using ICSharpCode.CodeConverter.Util;
using Xunit;

namespace CodeConverter.Tests.LanguageAgnostic
{
    public class ProjectFileTextEditorTests
    {

        [Fact]
        public void TogglesExistingValue()
        {
            var convertedProjFile = ProjectFileTextEditor.WithUpdatedDefaultItemExcludes(
                @"
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <DefaultItemExcludes>$(DefaultItemExcludes);$(SpaRoot)node_modules\**;$(ProjectDir)**\*.cs</DefaultItemExcludes>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
  </PropertyGroup>", "cs", "vb");

            Assert.Equal(Utils.HomogenizeEol(@"
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <DefaultItemExcludes>$(DefaultItemExcludes);$(SpaRoot)node_modules\**;$(ProjectDir)**\*.vb</DefaultItemExcludes>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
  </PropertyGroup>"),
            Utils.HomogenizeEol(convertedProjFile));
        }

        [Fact]
        public void InsertsIfNotPresent()
        {
            var convertedProjFile = ProjectFileTextEditor.WithUpdatedDefaultItemExcludes(
                @"
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <PropertyGroup>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
  </PropertyGroup>", "cs", "vb");

            Assert.Equal(Utils.HomogenizeEol(@"
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <DefaultItemExcludes>$(DefaultItemExcludes);$(ProjectDir)**\*.vb</DefaultItemExcludes>
  </PropertyGroup>
  <PropertyGroup>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
  </PropertyGroup>"),
                Utils.HomogenizeEol(convertedProjFile));
        }
    }
}

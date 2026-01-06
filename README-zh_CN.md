# BerryAIGen.Toolkit

BerryAIGen.Toolkit是一个专为AI生成图像设计的综合元数据索引器和查看器。它提供强大的工具，帮助您组织、搜索和管理不断增长的AI生成媒体集合。

## 目录

- [概述](#概述)
- [功能](#功能)
- [安装](#安装)
- [从源代码构建](#从源代码构建)
- [使用方法](#使用方法)
- [支持的格式](#支持的格式)
- [支持的元数据格式](#支持的元数据格式)
- [截图](#截图)
- [常见问题](#常见问题)
- [开发](#开发)

## 概述

BerryAIGen.Toolkit允许用户：
- 索引和搜索AI生成图像的元数据
- 使用评分、标签和相册组织图像
- 在用户友好的界面中查看详细元数据（PNGInfo）
- 在整个收藏中搜索和分析提示词
- 使用拖放功能管理图像库

## 功能

### 元数据管理
- **自动扫描**：扫描图像和视频以提取和索引提示词和其他元数据
- **元数据查看**：在预览窗格中轻松查看详细元数据
- **搜索功能**：使用强大的搜索过滤器搜索图像元数据

### 图像组织
- **评分系统**：为图像评分1-10
- **收藏标记**：将图像标记为收藏
- **NSFW支持**：通过关键词自动标记NSFW内容并模糊NSFW图像
- **自定义标签**：创建并应用自定义标签到图像
- **相册**：将图像组织到相册中，支持拖放操作

### 提示词分析
- **提示词库**：查看和搜索收藏中使用的所有提示词
- **使用统计**：查看特定提示词的使用频率
- **负面提示词分析**：分析整个收藏中的负面提示词
- **提示词到图像映射**：查看与特定提示词关联的所有图像

### 文件管理
- **文件夹视图**：使用熟悉的文件夹结构导航图像库
- **拖放支持**：拖动图像以移动它们（按住CTRL拖动以复制）
- **右键菜单命令**：使用上下文菜单移动、复制和管理图像

## 安装

### 系统要求
- **操作系统**：Windows 10/11
- **运行时**：[.NET 8.0 桌面运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)（包含.NET 8.0运行时）

### 下载和安装
1. 从[GitHub Releases](https://github.com/RupertAvery/BerryAIGen.Toolkit/releases/latest)页面下载最新版本
2. 展开**Assets**部分并下载zip文件（例如：`BerryAIGen.Toolkit.v1.x.zip`）
3. 将所有文件提取到您选择的文件夹
4. 运行`BerryAIGen.Toolkit.exe`启动应用程序

## 从源代码构建

### 前提条件
- **IDE**：Visual Studio 2022或更高版本
- **SDK**：[.NET 8.0 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)（包含运行时）

### 构建说明
1. 克隆此存储库
2. 在Visual Studio中打开解决方案文件`BerryAIGen.sln`
3. 使用**构建**菜单或按`Ctrl+Shift+B`构建解决方案
4. 或者，在项目根目录中从命令行运行`dotnet build`

### 发布
- 运行`publish.cmd`在`build`文件夹中创建发布版本

## 使用方法

### 入门
- 首次启动时，系统会提示您选择首选语言
- 使用设置菜单添加包含AI生成图像的文件夹
- 应用程序将自动扫描并索引您的图像
- 使用搜索栏通过元数据、标签或提示词查找图像

### 键盘快捷键
- `I`：在预览窗格中切换元数据可见性
- `Ctrl+F`：聚焦搜索栏
- `箭头键`：在图像之间导航

### 提示和技巧
有关更详细的使用信息，请参阅`document/develop`文件夹中的[开发文档](document/develop/develop.md)。

## 支持的格式

### 图像格式
- JPG/JPEG + EXIF
- PNG
- WebP
- MP4（视频）

### 元数据格式
- **AUTOMATIC1111及兼容**：Tensor.Art, SDNext
- **InvokeAI**：Dream/sd-metadata/invokeai_metadata
- **NovelAI**
- **Stable Diffusion**
- **EasyDiffusion**
- **Fooocus系列**：RuinedFooocus, Fooocus, FooocusMRE
- **Stable Swarm**

### 文本元数据
- .TXT元数据文件

## 支持的元数据格式

BerryAIGen.Toolkit可以从各种AI图像生成平台提取元数据。即使没有元数据的图像也可以使用评分和相册等功能！

## 截图

![主界面](https://github.com/Berry-Wahlberg/AIGenManager/assets/screenshots/main-interface.png)

![元数据视图](https://github.com/Berry-Wahlberg/AIGenManager/assets/screenshots/metadata-view.png)

## 常见问题

### 如何查看图像的元数据（PNGInfo）？
在预览窗格可见的情况下，在缩略图视图中或预览窗格获得焦点时按`I`键，显示或隐藏元数据。您也可以点击预览窗格右下角的眼睛图标。

### 什么是重建元数据，何时应该使用它？
重建元数据将重新扫描所有图像，并使用找到的任何新的或更新的元数据更新数据库。它不会影响您的自定义标签（评分、收藏、NSFW）。当BerryAIGen.Toolkit的新版本增加了对您现有图像中存在的元数据格式的支持时，您才需要使用此功能。

### 我可以将图像移动到不同的文件夹吗？
要在保留元数据的情况下将图像移动到BerryAIGen文件夹内的不同位置，应使用**右键 > 移动**命令。这确保BerryAIGen.Toolkit处理图像的移动，并在移动时保持所有特定于工具包的元数据（收藏、评分、NSFW标签）完整无损。使用资源管理器或其他应用程序移动文件将导致元数据丢失。

## 开发

在`document/develop`文件夹中提供了全面的开发文档。此文档包括：
- 技术规范
- 架构图
- 实现细节
- 开发工作流程
- API文档
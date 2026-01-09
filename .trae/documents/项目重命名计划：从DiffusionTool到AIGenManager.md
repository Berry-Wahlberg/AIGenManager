# 项目重命名与品牌更新计划

## 1. 项目概述

需要将原项目DiffusionTool重命名为：

* 中文名：花花 AI 绘图管家

* 英文名：Berry AIGen ArtManager

* 仓库名：AIGenManager

* 版本号：v0.1beta

同时需要在介绍界面和其他合适位置插入GitHub链接：`https://github.com/Berry-Wahlberg/AIGenManager`

## 2. 重命名范围

### 2.1 解决方案和项目文件

* 重命名解决方案文件：`Diffusion.Toolkit.sln` → `AIGenManager.sln`

* 更新解决方案文件中的项目名称和路径

### 2.2 应用程序信息

* 修改应用程序标题：根据语言显示中文名或英文名

* 修改版本号为v0.1beta

* 更新窗口标题和相关UI元素

### 2.3 本地化文件

* 更新所有语言文件中的应用程序名称

* 确保中文显示"花花 AI 绘图管家"，英文显示"Berry AIGen Art Manager"

### 2.4 GitHub链接插入

* 在WelcomeWindow的介绍页面（Step 1）添加GitHub链接

* 在About对话框或帮助菜单中添加GitHub链接

* 在合适的UI位置添加GitHub链接

## 3. 实施步骤

### 3.1 更新本地化文件

1. 修改所有语言文件中的应用程序名称：

   * 中文(zh-CN.json)：将"Diffusion Toolkit"替换为"花花 AI 绘图管家"

   * 英文(en-US.json)：将"Diffusion Toolkit"替换为"Berry AIGen Art Manager"

   * 其他语言文件类似更新

2. 在语言文件中添加GitHub链接相关文本

### 3.2 更新WelcomeWindow

1. 在WelcomeWindow\.xaml的介绍页面（Step 1）添加GitHub链接

2. 使用Hyperlink控件，确保链接可点击

3. 添加适当的文本说明

### 3.3 更新应用程序标题和版本

1. 修改MainWindow\.xaml的Title属性，使用本地化字符串

2. 更新AppInfo.cs或相关文件中的版本号为v0.1beta

3. 修改日志输出中的应用程序名称

### 3.4 添加GitHub链接到其他位置

1. 检查Help菜单，添加GitHub链接

2. 检查About对话框，添加GitHub链接

3. 在适当的UI位置添加GitHub链接

### 3.5 测试和验证

1. 编译项目，检查是否有编译错误

2. 运行应用程序，验证：

   * 介绍界面显示GitHub链接

   * 应用程序标题正确显示

   * 版本号显示为v0.1beta

   * GitHub链接可正常点击

## 4. 预期结果

* 应用程序名称根据语言正确显示

* 版本号显示为v0.1beta

* 介绍界面和其他合适位置显示GitHub链接

* 所有功能正常工作

* 无编译错误

## 5. 风险评估

### 5.1 低风险

* 本地化文件更新不完整：可以通过测试不同语言验证

* GitHub链接格式错误：可以通过点击测试验证

* 版本号更新不完整：可以通过查看日志和UI验证

### 5.2 中风险

* 应用程序标题显示错误：可以通过测试不同语言验证

* 链接位置不合适：可以根据用户反馈调整

## 6. 后续工作

* 测试所有功能

* 根据用户反馈调整GitHub链接位置

* 考虑更新图标和资源文件以匹配新品牌

* 准备发布说明


import os
import re

# 定义需要处理的目录
root_dir = r"E:\Develop\AIGenManager"

# 排除的目录
exclude_dirs = ["bin", "obj", "Properties", ".git"]

# 正则表达式匹配C# 14.0属性语法
property_pattern = re.compile(r'''(\s*)(public|private|protected|internal)\s+(\w+(\s*<[^>]+>)?(\[\])?)\s+(\w+)\s*\n(\s*){\s*\n(\s*)get;\s*\n(\s*)set\s*=>\s*(\w+)\s*\(\s*ref\s+field\s*,\s*value\s*\);\s*\n(\s*)}''', re.MULTILINE)

def process_file(file_path):
    """处理单个文件"""
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        # 查找所有匹配的属性
        matches = list(property_pattern.finditer(content))
        if not matches:
            return False
        
        # 从后往前处理，避免替换影响后续匹配
        matches.reverse()
        
        modified = False
        new_content = content
        
        for match in matches:
            indent = match.group(1)
            access_modifier = match.group(2)
            type_name = match.group(3)
            property_name = match.group(6)
            get_indent = match.group(9)
            set_indent = match.group(10)
            method_name = match.group(11)
            end_indent = match.group(12)
            
            # 生成支持字段名
            field_name = f"_{property_name[:1].lower()}{property_name[1:]}"
            
            # 构建新的属性定义
            new_property = f"{indent}private {type_name} {field_name};\n"
            new_property += f"{indent}{access_modifier} {type_name} {property_name}\n"
            new_property += f"{indent}{{\n"
            new_property += f"{get_indent}get => {field_name};\n"
            new_property += f"{set_indent}set => {method_name}(ref {field_name}, value);\n"
            new_property += f"{end_indent}}}"
            
            # 替换原属性
            new_content = new_content[:match.start()] + new_property + new_content[match.end():]
            modified = True
        
        if modified:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(new_content)
            print(f"Fixed: {file_path}")
        
        return modified
    
    except Exception as e:
        print(f"Error processing {file_path}: {e}")
        return False

def process_directory(dir_path):
    """处理目录及其子目录"""
    total_files = 0
    fixed_files = 0
    
    for root, dirs, files in os.walk(dir_path):
        # 排除不需要处理的目录
        dirs[:] = [d for d in dirs if d not in exclude_dirs]
        
        for file in files:
            if file.endswith('.cs'):
                total_files += 1
                file_path = os.path.join(root, file)
                if process_file(file_path):
                    fixed_files += 1
    
    return total_files, fixed_files

if __name__ == "__main__":
    print("开始修复C# 14.0属性语法问题...")
    total, fixed = process_directory(root_dir)
    print(f"修复完成！总共处理了 {total} 个文件，修复了 {fixed} 个文件中的属性。")

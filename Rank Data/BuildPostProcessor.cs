using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildPostProcessor : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPostprocessBuild(BuildReport report)
    {
        // ����� ���ø����̼��� ���丮 ���
        string buildPath = Path.GetDirectoryName(report.summary.outputPath);

        //// ��� �÷����� ���丮 ���
        //string targetFolderDir = Path.Combine(buildPath, "Rank Data");

        //// �߰��� ������ ���� ���
        //string sourceFilePath = Path.Combine("Assets", "Orbbec", "Plugins", "x86_64", "OrbbecSand.dll");

        //// ����� ������ ��� ���
        //string targetFilePath = Path.Combine(targetPluginDir, "OrbbecSand.dll");

        //// ��� ���丮 ����
        //Directory.CreateDirectory(targetFolderDir);

        //// ���� ���� (����� ���θ� true�� ����)
        //File.Copy(sourceFilePath, targetFilePath, true);

        // 'Assets/Editor' ���� ���� ���ϵ��� 'additionalFiles' ������ ����
        string sourceEditorDir = Path.Combine("Assets", "Editor");
        string targetAdditionalFilesDir = Path.Combine(buildPath, "Rank Data");

        // additionalFiles ���� ����
        Directory.CreateDirectory(targetAdditionalFilesDir);

        // Assets/Editor ���� ���� ��� ������ ����
        foreach (string file in Directory.GetFiles(sourceEditorDir))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(targetAdditionalFilesDir, fileName);
            File.Copy(file, destFile, true);
        }
    }
}

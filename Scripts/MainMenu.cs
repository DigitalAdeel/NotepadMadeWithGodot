using Godot;
using System;
using System.Collections.Generic;

public class MainMenu : Control
{
    [Export] public NodePath Editor;
    [Export] public NodePath Menu;
    [Export] public NodePath AcceptDialog;
    [Export] public NodePath MenuItems;
    [Export] public NodePath saveFileDialog;
    [Export] public NodePath openFileDialog;
    [Export] public NodePath FontSlider;


    public override void _Ready()
    {
    }

    public void OnMenuButtonClicked()
    {
        var items = GetNode<PopupMenu>(MenuItems);
        items.ShowOnTop = true;
        items.Show();
    }

    TextEdit editor;
    void on_MenuItems_id_pressed(int Id)
    {
        editor = GetNode<TextEdit>(Editor);
        switch (Id)
        {
            case 0: editor.Text = ""; break;
            case 1: LoadFile(); break;
            case 2: SaveFile(); break;
            case 3: editor.Text = @"My name is Muhammad Adeel Abid. 
I am a self learned C# .NET Developer and been programming since Visual Studio 2015. 
I've developed many projects while practicing C# from developing a simple notepad to complicated UI Designs. 
Professionally speaking, am working as a freelancer on Fiverr since SEP_2021 and done projects of simple level to complex level. 
Feel free to contact me before placing a order. 
Your satisfaction (yes, you whose reading this atm 😅) is my utmost priority..."; break;
            case 4: GetTree().Quit(); break;
            default: break;
        }
    }

    private void LoadFile()
    {
        var ofd = GetNode<FileDialog>(openFileDialog);
        ofd.Show();
    }
    private void SaveFile()
    {
        var sfd = GetNode<FileDialog>(saveFileDialog);
        sfd.Show();
    }

    string DirLoc;
    private void SaveFileDialog_confirmed()
    {

        var sfd = GetNode<FileDialog>(saveFileDialog);
        DirLoc = sfd.CurrentDir;
        var accept = GetNode<AcceptDialog>(AcceptDialog);
        accept.Show();
    }

    public List<string> Extensions = new List<string>(){
        ".cs",".txt",".html",".cpp",".jar"
    };


    void OnAcceptDialog_confirmed(){
        editor = GetNode<TextEdit>(Editor);
        var lineEdit = GetNode<LineEdit>(AcceptDialog+"/Panel/LineEdit");
        var exes = GetNode<OptionButton>(AcceptDialog+"/Panel/Extensions");
        if(!string.IsNullOrWhiteSpace(lineEdit.Text)){
            System.IO.File.WriteAllText($"{DirLoc}/{lineEdit.Text}{exes.GetItemText(exes.Selected)}", editor.Text);
        }
    }
    void FileDialog_file_selected(string path)
    {
        editor = GetNode<TextEdit>(Editor);
        var ofd = GetNode<FileDialog>(openFileDialog);
        editor.Text = System.IO.File.ReadAllText(path);
    }

    void OnVSlider_mouse_entered(){
        var vslider = GetNode<VSlider>(FontSlider);
        vslider.Visible = true;
    }
    
    void OnVSlider_mouse_leave(){
        var vslider = GetNode<VSlider>(FontSlider);
        //vslider.Visible = false;
    }
    [Export]
    public DynamicFont FontFile;
    void OnVSlider_value_changed(float value){
        //var vslider = GetNode<VSlider>(FontSlider);
        FontFile.Size = Convert.ToInt32(value);

    }

}
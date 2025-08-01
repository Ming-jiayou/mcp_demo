using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace MCP_Studio.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentPage;

    public MainWindowViewModel()
    {
        // 默认显示Chat页面
        CurrentPage = new ChatViewModel();
    }

    [RelayCommand]
    private void NavigateToChat()
    {
        CurrentPage = new ChatViewModel();
    }

    [RelayCommand]
    private void NavigateToSettings()
    {
        CurrentPage = new SettingsViewModel();
    }

    [RelayCommand]
    private void NavigateToMCPSettings()
    {
        CurrentPage = new MCPSettingsViewModel();
    }

    [RelayCommand]
    private void NavigateToTTS()
    {
        CurrentPage = new TTSViewModel();
    }
}

namespace Projeto.Components;

public partial class Card : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(Card), string.Empty);

    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(Card), string.Empty,
                                propertyChanged: OnSubtitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public bool HasSubtitle => !string.IsNullOrWhiteSpace(Subtitle);

    static void OnSubtitleChanged(BindableObject bindable, object oldValue, object newValue)
        => ((Card)bindable).OnPropertyChanged(nameof(HasSubtitle));

    public Card()
    {
        InitializeComponent();
    }
}
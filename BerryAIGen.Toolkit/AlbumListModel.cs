using System.Collections.Generic;
using BerryAIGen.Common;
using System.Windows.Input;
using BerryAIGen.Database.Models;
using BerryAIGen.Toolkit.Models;

namespace BerryAIGen.Toolkit;

public class AlbumListModel : BaseNotify
{
    public ICommand Escape { get; set; }

    public bool IsNewAlbum
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsExistingAlbum
    {
        get;
        set => SetField(ref field, value);
    }

    public Album? SelectedAlbum
    {
        get;
        set => SetField(ref field, value);
    }

    public string AlbumName
    {
        get;
        set => SetField(ref field, value);
    }

    public IEnumerable<Album> Albums
    {
        get;
        set => SetField(ref field, value);
    }

    public bool CanClickOk
    {
        get;
        set => SetField(ref field, value);
    }
}








using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;
using Terraria.ModLoader.Exceptions;

namespace ProtoMod.Core;

public class PathRedirectContentSource(IContentSource source) : IContentSource
{
    private readonly Dictionary<string, string> _directoryRedirects = new();

    public IContentValidator ContentValidator
    {
        get => source.ContentValidator;
        set => source.ContentValidator = value;
    }
    
    public RejectedAssetCollection Rejections => source.Rejections;
    
    public IEnumerable<string> EnumerateAssets() => source.EnumerateAssets().Select(RedirectPath);
    
    public string GetExtension(string assetName) => source.GetExtension(RedirectPath(assetName));
    
    public Stream OpenStream(string fullAssetName) => source.OpenStream(RedirectPath(fullAssetName));
    
    public void MapDirectory(string fromDir, string toDir)
    {
        _directoryRedirects[fromDir] = toDir;
    }
    
    private string RedirectPath(string path)
    {
        foreach ((string fromDir, string toDir) in _directoryRedirects)
        {
            if (path.StartsWith(fromDir))
                return toDir + path[fromDir.Length..];
        }
        
        return path;
    }
}
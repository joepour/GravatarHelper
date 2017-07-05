# GravatarHelper
Simple class to generate Gravatar Urls and Hashes.

## Documentation

Get Gravatar Hash
-----------------
Sometimes you just need the Gravatar hash.

```
var hash = GravatarHelper.GetGravatarHash("john.doe@gmail.com");
```

Get Gravatar Url
----------------
Get Gravatar url with https protocol

```
var url = GravatarHelper.GetGravatarUrl("john.doe@gmail.com");
```

or without the protocol

```
var url = GravatarHelper.GetGravatarUrl("john.doe@gmail.com", false);
```

Configure Gravatar Helper
------------------

```
var url = GravatarHelper.GetGravatarUrl("john.doe@gmail.com", new GravatarHelperOptions()
{
    DefaultImageUrl = "https://www.gravatar.com/avatar/00000000000000000000000000000000", // provide fallback image
    Size = 400, // image size
    Rating = "g", // g, pg, r, x
    Protocol = "https", // if you need to specify non-secure
    ForceDefault = true // I honestly don't even know why this is an option. Does what it says on the tin.
});

//https://gravatar.com/avatar/e13743a7f1db7f4246badd6fd6ff54ff?s=400&d=https://www.gravatar.com/avatar/00000000000000000000000000000000&f=y&r=g
```


Blazor Server .NET Core 7

![alt text](https://github.com/joramkimata/frontend-investments-kiper/blob/21128b003574c294c53cbf6180a347e5ac04c94d/login.png)

```
<ItemGroup>
    <PackageReference Include="GraphQL" Version="7.4.1" />
    <PackageReference Include="GraphQL.Client" Version="6.0.0" />
    <PackageReference Include="GraphQL.Client.Serializer.Newtonsoft" Version="6.0.0" />
</ItemGroup>

```

Get Machine IP

``bash
hostname -I | awk '{print $1}'
``
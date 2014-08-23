AutoFixture will likely need to have a bind redirect to xunit.extensions in app.config:

<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="xunit.extensions"
                          publicKeyToken="8d05b1bb7a6fdb6c"
                          culture="neutral" />
        <bindingRedirect oldVersion="1.6.1.1521"
                         newVersion="1.9.0.1566"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>

# CredentialManagement

WdfSolution 任務的共用規範見 [AGENTS.md](../../../../WdfSolution/AGENTS.md)。

封裝 Windows Credential 管理與 SecureString／native interop。

- 閱讀入口：[Credential.cs](Credential.cs)、[CredentialSet.cs](CredentialSet.cs)、[NativeMethods.cs](NativeMethods.cs)、[SecureStringHelper.cs](SecureStringHelper.cs)。
- Credential 實作 IDisposable；修改 native 呼叫時維持 unmanaged memory 與 SecureString 的清理配對。


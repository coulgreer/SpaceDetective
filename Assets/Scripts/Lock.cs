public class Lock {
    public KeyId TargetKeyId { get; private set; }
    public bool IsLocked { get; private set; }

    public Lock(KeyId targetKeyId, bool isLocked) {
        this.TargetKeyId = targetKeyId;
        this.IsLocked = isLocked;
    }

    public Lock(KeyId targetKeyId)
        : this(targetKeyId, true) { }

    public void UseKey(IKey key) {
        if (IsLocked) {
            IsLocked = !TargetKeyId.Equals(key.KeyId);
        }
    }
}

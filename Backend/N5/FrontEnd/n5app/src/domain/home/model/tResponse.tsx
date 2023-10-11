export class TResponse<T> {
  public succeeded?: boolean;
  public errorCode?: number;
  public error?: string;
  public data: T;
  public count?: number;

  public constructor(
    value?: T,
    isSuccess?: boolean,
    count: number = 0,
    error?: string,
  ) {
    if (value) {
      this.succeeded = true;
      this.data = value;
    } else {
      this.succeeded = isSuccess ?? false;
      this.error = error ?? '';
      this.data = value as T;
    }
    this.count = count;
    Object.freeze(this);
  }

  public isSuccess() {
    return this.succeeded;
  }

  public isFailure() {
    return !this.succeeded;
  }

  public getValue(): T {
    if (!this.isSuccess()) {
      console.log(this.error);
      throw new Error(
        "Can't get the value of an error result. Use 'errorValue' instead.",
      );
    }
    return this.data;
  }
}

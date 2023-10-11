import { ErrorUser } from "domain/home/enums/ErrorUser";

/**
 * General error
 */
export class GeneralError extends Error {
  public code: number | undefined = undefined;

  constructor(message: string, code: ErrorUser | undefined = undefined) {
    super();
    this.name = 'GeneralError';
    this.message = message;
    this.code = code;
  }
  public getCode(): ErrorUser | undefined {
    return this.code;
  }
}

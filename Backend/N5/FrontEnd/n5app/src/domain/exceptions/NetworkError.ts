import { GENERAL_NETWORK_ERROR } from "domain/home/types/GeneralMessageTypes";

/**
 * Network error
 */
export class NetworkError extends Error {
  private code: number | undefined = undefined;

  constructor(message: string, code: number | undefined = undefined) {
    super();
    this.name = GENERAL_NETWORK_ERROR;
    this.message = message;
    this.code = code;
  }
  public getCode(): number | undefined {
    return this.code;
  }
}

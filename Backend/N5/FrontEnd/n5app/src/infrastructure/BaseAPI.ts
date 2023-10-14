import axios, {AxiosInstance} from 'axios';
import { NetworkError } from '../domain/exceptions';
import { USER_CONFIGURATION_PROXY_BASE } from '../domain/home/types/UserPermissionTypes';

/* Class that serves as a base for making HTTP requests using the Axios library.
It defines two methods, `get` and `post`, which make GET and POST requests respectively. The class
has a `baseUrl` property that is set to the API base URL specified in the `Config` object. It also
has an `axiosInstance` property that is an instance of the Axios library with a timeout of 2000ms.
The class is exported as the default export, which means that it can be imported and used in other
modules. The class is also marked as abstract, which means that it cannot be instantiated directly,
but must be extended by other classes that implement its methods. */
export default abstract class BaseAPI {
  protected readonly baseUrl: string | undefined;
  private readonly axiosInstance: AxiosInstance | any = null;

  constructor() {
    this.baseUrl = USER_CONFIGURATION_PROXY_BASE;
    this.axiosInstance = axios.create({timeout: 2000});
  }

  /**
   * Function that makes an HTTP GET request using Axios and returns the response
   * data as a Promise.
   * @param {string} url - The endpoint URL to which the GET request will be sent.
   * @param {any} [params] - An optional object containing key-value pairs to be sent as query parameters
   * in the GET request.
   * @param {any} [headers] - The headers parameter is an optional object that contains additional HTTP
   * headers to be sent with the request. These headers can be used to provide authentication
   * credentials, specify the content type of the request, or provide other information that the server
   * may need to process the request. If no headers are provided, the request will
   * @returns This function returns a Promise that resolves to a generic type T. The type T is specified
   * when the function is called.
   */
  public async get<T>(url: string, params?: any, headers?: any): Promise<T> {
    try {
      let newIUrl = `${this.baseUrl}${url}`;
      const {data} = await axios.get<T>(newIUrl, {
        params,
        headers,
      });
      return data;
    } catch (error: any) {
      let errorMessage = error.response?.data;
      throw new NetworkError(errorMessage, error.response?.status);
    }
  }

  /**
   * Function that sends a POST request using Axios and returns the response data
   * or throws an error.
   * @param {string} url - The endpoint URL to which the POST request will be sent.
   * @param {any} [dataObject] - The data to be sent as the request body. It can be of any type, but it
   * will be serialized as JSON by default.
   * @param {any} [params] - An optional object containing key-value pairs to be sent as query
   * parameters in the request URL. For example, if params = {id: 123, name: 'John'}, the resulting URL
   * will be something like: http://example.com/api? id=123&name=John.
   * @param {any} [headers] - The headers parameter is an optional object that contains additional HTTP
   * headers to be sent with the request. These headers can include things like authentication tokens,
   * content type, and accept headers.
   * @returns a Promise that resolves to the response data from an HTTP POST request made using the
   * Axios library. The response data is of type `T`, which is a generic type parameter that can be
   * specified when calling the function. If an error occurs during the request, the function will
   * throw either a `NetworkError` or a `GeneralError` depending on the type of error.
   */
  public async post<T>(
    url: string,
    dataObject?: any,
    params?: any,
    headers?: any,
  ): Promise<any> {
    try {
      const {data} = await axios.post<T>(`${this.baseUrl}${url}`, dataObject, {
        params,
        headers,
      });
      return data;
    } catch (error: any) {
      let errorMessage = error.response?.data;

      throw new NetworkError(errorMessage, error.response?.status);
    }
  }
}

import { UserPermissionResponseModel } from "../model/GetUserPermissionResponseModel";

export const HomeState = {
  data: undefined as UserPermissionResponseModel[] | undefined,
  loading: false,
};

export default HomeState;

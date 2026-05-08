export type Doctor = {
  id: string;
  fullName: string;
  email: string;
  specialization: string;
  licenseNumber: string;
  licenseExpiryDate: string;
  status: "Active" | "Expired" | "Suspended";
  createdDate: string;
};

export type ApiResponse<T> = {
  success: boolean;
  message: string;
  data: T;
};

export type CreateDoctorRequest = {
  fullName: string;
  email: string;
  specialization: string;
  licenseNumber: string;
  licenseExpiryDate: string;
};

export type PagedResult<T> = {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
};

export type UpdateDoctorRequest = CreateDoctorRequest;

export type UpdateDoctorStatusRequest = {
  status: number;
};
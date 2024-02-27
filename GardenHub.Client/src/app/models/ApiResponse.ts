export interface ApiResponse<T> {
    data: T[];
    successful: boolean;
    message: string;
}
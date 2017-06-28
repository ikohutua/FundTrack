/**
 * View modelm used for user password changing
 */
export class ChangePasswordViewModel {
    public login: string;
    public oldPassword: string;
    public newPassword: string;
    public newPasswordConfirmation: string;
    public errorMessage: string;
}
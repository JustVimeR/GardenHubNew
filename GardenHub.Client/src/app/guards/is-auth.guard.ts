import { CanActivateFn } from '@angular/router';

export const isAuthGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem('auth-token');
  if (token) {
    return true;
  }
  return false
};
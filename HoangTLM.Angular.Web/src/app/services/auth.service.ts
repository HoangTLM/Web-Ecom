import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<{ token: string }> {
    // Đổi URL này thành endpoint thực tế của bạn
    return this.http.post<{ token: string }>('/api/auth/login', { username, password });
  }

  refreshToken() {
    return this.http.post<{ token: string }>('/api/auth/refresh-token', {});
  }
} 
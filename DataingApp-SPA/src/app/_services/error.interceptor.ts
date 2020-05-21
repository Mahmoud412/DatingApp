import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHeaderResponse, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
  return next.handle(req).pipe(
      catchError(error => {
          if (error.status === 401){
              return throwError(error.statusText);
          }
        // 500 error handler
          if (error instanceof HttpErrorResponse ){
            const applicationError = error.headers.get('Application-Error');
            if(applicationError){
                return throwError(applicationError);
            }

            // model stat errors

            const serverError = error.error;

            let modelStateErrors = '';

            if (serverError.errors && typeof serverError.errors === 'object')
            {
                for (const Key in serverError.errors)
                {
                    if (serverError.errors[Key]) {

                         modelStateErrors += serverError.errors[Key] + '\n';
                    }
                }
            }
            return throwError ( modelStateErrors || serverError || 'Server Error');
          }
      })
    );
  }
}
// we do this be cuz we need to add our errors interceptor to our provider array
export const ErrorInterceptorProvider = {
    provide : HTTP_INTERCEPTORS,
    useClass : ErrorInterceptor,
    multi : true
};

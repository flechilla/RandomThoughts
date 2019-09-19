// This file contains the definition of the URls


export const AppDomainUrl = 'http://localhost:5001';

// Security
export const GetXsrfTokenUrl = `${AppDomainUrl}/security/AntiForgeryToken`;
export const XsrfPostUrl = `${AppDomainUrl}/values`;
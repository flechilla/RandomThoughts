// This file contains the definition of the URls


export const AppDomainUrl = 'http://localhost:5001/api/v1/';

// Security
export const GetXsrfTokenUrl = `${AppDomainUrl}/security/AntiForgeryToken`;
export const XsrfPostUrl = `${AppDomainUrl}/values`;

//Thought Holes
export const GetAllPublicHoles = `${AppDomainUrl}/ThoughtHoles/GetAllPublic`;
export const GetAllPersonalHoles = `${AppDomainUrl}/ThoughtHoles/GetAllPersonalHoles`;
// Needs and Id in the last segment of the URL
export const GetHoleById = `${AppDomainUrl}/ThoughtHoles/Get`;
export const PostHole = `${AppDomainUrl}/ThoughtHoles/Post`;
// Needs and Id in the last segment of the URL
export const PutHole = `${AppDomainUrl}/ThoughtHoles/Put`;
// Needs and Id in the last segment of the URL
export const DeleteHole = `${AppDomainUrl}/ThoughtHoles/Delete`;


// This file contains the definition of the URls


export const AppDomainUrl = 'http://localhost:5001/api/v1/';

// Security
export const GetXsrfTokenUrl = `${AppDomainUrl}/security/AntiForgeryToken`;
export const XsrfPostUrl = `${AppDomainUrl}/values`;

//Thought Holes
export const GetAllPublicHoles = `${AppDomainUrl}/ThoughtHoles/GetAllPublic`;
export const GetAllPersonalHoles = `${AppDomainUrl}/ThoughtHoles/GetAllPersonalHoles`;
// Needs an Id in the last segment of the URL
export const GetHoleById = `${AppDomainUrl}/ThoughtHoles/Get`;
export const PostHole = `${AppDomainUrl}/ThoughtHoles/Post`;
// Needs an Id in the last segment of the URL
export const PutHole = `${AppDomainUrl}/ThoughtHoles/Put`;
// Needs an Id in the last segment of the URL
export const DeleteHole = `${AppDomainUrl}/ThoughtHoles/Delete`;

// Thoughts
export const GetUserThoughts = `${AppDomainUrl}/Thoughts/GetUserThoughts`;
export const GetThought = `${AppDomainUrl}/Thoughts/GetThought`;
export const PostThought = `${AppDomainUrl}/Thoughts/PostThought`;
// Needs the THoughtId Id in the last segment of the URL
export const PutThought = `${AppDomainUrl}/Thoughts/PutThought`;
// Needs the THoughtId Id in the last segment of the URL
export const DeleteThought = `${AppDomainUrl}/Thoughts/DeleteThought`;
// Needs the THoughtId Id in the last segment of the URL
export const GetAllComments = `${AppDomainUrl}/Thoughts/GetAllComments`;
// Needs the THoughtId Id in the last segment of the URL
export const PutComment = `${AppDomainUrl}/Thoughts/PutComment`;
// Needs the THoughtId Id in the last segment of the URL
export const DeleteComment = `${AppDomainUrl}/Thoughts/DeleteComment`;
export const PostComment = `${AppDomainUrl}/Thoughts/PostComment`;
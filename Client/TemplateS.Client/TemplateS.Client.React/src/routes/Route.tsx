import React from 'react';
import {
    Route as ReactDOMRoute,
    RouteProps as ReactDOMRouteProps,
    Redirect,
} from 'react-router-dom';

import { useAuth } from '../hooks/auth';

interface RouteProps extends ReactDOMRouteProps {
    isPrivate?: boolean;
    component: React.ComponentType;
    Both?: boolean;
}

const Route: React.FC<RouteProps> = ({
    isPrivate = false,
    Both = false,
    component: Component,
    ...rest
}) => {
    const { user } = useAuth();

    return (
        <ReactDOMRoute
            {...rest}
            render={({ location }) => {
                return ((isPrivate === !!user) || Both) ? (
                    <Component />
                ) : (
                    <Redirect
                        to={{
                            pathname: isPrivate ? '/' : '/databaserepositories',
                            state: { from: location },
                        }}
                    />
                );
            }}
        />
    );
};

export default Route;

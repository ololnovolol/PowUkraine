import React, { useEffect } from 'react';
import ReactDOM from 'react-dom';
import { CSSTransition } from 'react-transition-group';
import '../../style/modals/modal.css';
import styled from 'styled-components';

const BgButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    weight: 100px;
    border-color: #fff;
    color: #f0a30a;
    border-radius: 5px;
    font-size: 16px;
    margin-top: 1rem;
    margin-left: 42%;
    &:hover {
        color: #fff;
        background: #6d8764;
        cursor: pointer;
    }
`;

const Modal = props => {
    const closeOnEscapeKeyDown = e => {
        if ((e.charCode || e.keyCode) === 27) {
            props.onClose();
        }
    };

    useEffect(() => {
        document.body.addEventListener('keydown', closeOnEscapeKeyDown);
        return function cleanup() {
            document.body.removeEventListener('keydown', closeOnEscapeKeyDown);
        };
    }, [closeOnEscapeKeyDown]);

    return ReactDOM.createPortal(
        <CSSTransition
            in={props.show}
            unmountOnExit
            timeout={{ enter: 0, exit: 300 }}>
            <div className="modal" onClick={props.onClose}>
                <div
                    className="modal-content"
                    onClick={e => e.stopPropagation()}>
                    <div className="modal-body">{props.children}</div>
                    <div className="modal-footer">
                        <BgButton onClick={props.onClose} type="submit">
                            OK
                        </BgButton>
                    </div>
                </div>
            </div>
        </CSSTransition>,
        document.getElementById('root'),
    );
};

export default Modal;

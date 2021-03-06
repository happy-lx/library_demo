PGDMP         %                x            library_demo2    10.10    10.10                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false                       1262    25388    library_demo2    DATABASE     �   CREATE DATABASE library_demo2 WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Chinese (Simplified)_China.936' LC_CTYPE = 'Chinese (Simplified)_China.936';
    DROP DATABASE library_demo2;
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false                       0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    3                        3079    12924    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false                       0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            �            1259    25389 	   book_info    TABLE     �   CREATE TABLE public.book_info (
    book_id bigint NOT NULL,
    book_name character varying(50) NOT NULL,
    author character varying(20) NOT NULL,
    publisher character varying(30) NOT NULL,
    self_info character varying(500)
);
    DROP TABLE public.book_info;
       public         postgres    false    3            �            1259    25395    book_num    TABLE     �   CREATE TABLE public.book_num (
    book_id bigint NOT NULL,
    remain bigint,
    CONSTRAINT book_num_remain_check CHECK ((remain >= 0))
);
    DROP TABLE public.book_num;
       public         postgres    false    3            �            1259    25399    record    TABLE     �   CREATE TABLE public.record (
    username character varying(12) NOT NULL,
    book_id bigint NOT NULL,
    borrow_time date NOT NULL,
    return_time date
);
    DROP TABLE public.record;
       public         postgres    false    3            �            1259    25402 	   user_info    TABLE       CREATE TABLE public.user_info (
    username character varying(12) NOT NULL,
    name character varying(5) NOT NULL,
    mail character varying(30),
    address character varying(50),
    occupy character varying(10),
    sex character varying(4),
    balance numeric,
    self_info character varying(200),
    CONSTRAINT user_info_balance_check CHECK ((balance >= (0)::numeric)),
    CONSTRAINT user_info_sex_check CHECK (((sex)::text = ANY (ARRAY[('男'::character varying)::text, ('女'::character varying)::text])))
);
    DROP TABLE public.user_info;
       public         postgres    false    3            �            1259    25410 	   user_pass    TABLE     �   CREATE TABLE public.user_pass (
    username character varying(12) NOT NULL,
    user_password character varying(10000) NOT NULL
);
    DROP TABLE public.user_pass;
       public         postgres    false    3                      0    25389 	   book_info 
   TABLE DATA               U   COPY public.book_info (book_id, book_name, author, publisher, self_info) FROM stdin;
    public       postgres    false    196   }                 0    25395    book_num 
   TABLE DATA               3   COPY public.book_num (book_id, remain) FROM stdin;
    public       postgres    false    197   �       	          0    25399    record 
   TABLE DATA               M   COPY public.record (username, book_id, borrow_time, return_time) FROM stdin;
    public       postgres    false    198   �       
          0    25402 	   user_info 
   TABLE DATA               c   COPY public.user_info (username, name, mail, address, occupy, sex, balance, self_info) FROM stdin;
    public       postgres    false    199   @                 0    25410 	   user_pass 
   TABLE DATA               <   COPY public.user_pass (username, user_password) FROM stdin;
    public       postgres    false    200   �        �
           2606    25417    book_info book_info_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public.book_info
    ADD CONSTRAINT book_info_pkey PRIMARY KEY (book_id);
 B   ALTER TABLE ONLY public.book_info DROP CONSTRAINT book_info_pkey;
       public         postgres    false    196            �
           2606    25419    book_num book_num_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.book_num
    ADD CONSTRAINT book_num_pkey PRIMARY KEY (book_id);
 @   ALTER TABLE ONLY public.book_num DROP CONSTRAINT book_num_pkey;
       public         postgres    false    197            �
           2606    25421    user_info user_info_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.user_info
    ADD CONSTRAINT user_info_pkey PRIMARY KEY (username);
 B   ALTER TABLE ONLY public.user_info DROP CONSTRAINT user_info_pkey;
       public         postgres    false    199            �
           2606    25423    user_pass user_pass_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.user_pass
    ADD CONSTRAINT user_pass_pkey PRIMARY KEY (username);
 B   ALTER TABLE ONLY public.user_pass DROP CONSTRAINT user_pass_pkey;
       public         postgres    false    200            �
           2606    25424    book_num book_num_book_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.book_num
    ADD CONSTRAINT book_num_book_id_fkey FOREIGN KEY (book_id) REFERENCES public.book_info(book_id) ON UPDATE CASCADE ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.book_num DROP CONSTRAINT book_num_book_id_fkey;
       public       postgres    false    196    2691    197            �
           2606    25429    record record_book_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.record
    ADD CONSTRAINT record_book_id_fkey FOREIGN KEY (book_id) REFERENCES public.book_info(book_id) ON UPDATE CASCADE ON DELETE CASCADE;
 D   ALTER TABLE ONLY public.record DROP CONSTRAINT record_book_id_fkey;
       public       postgres    false    196    2691    198            �
           2606    25434    record record_username_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.record
    ADD CONSTRAINT record_username_fkey FOREIGN KEY (username) REFERENCES public.user_info(username) ON UPDATE CASCADE ON DELETE CASCADE;
 E   ALTER TABLE ONLY public.record DROP CONSTRAINT record_username_fkey;
       public       postgres    false    199    198    2695            �
           2606    25439 !   user_pass user_pass_username_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_pass
    ADD CONSTRAINT user_pass_username_fkey FOREIGN KEY (username) REFERENCES public.user_info(username) ON UPDATE CASCADE ON DELETE CASCADE;
 K   ALTER TABLE ONLY public.user_pass DROP CONSTRAINT user_pass_username_fkey;
       public       postgres    false    2695    199    200               �   x���=N�@���SL��C3���*^O�;V��a	DAA�B�
S .c[9k
�4i��yߛ��Ϝ)3�\k�E�.��)E;��cƥVG���*�1f<�1$$���k/���F���8�b1\�0nA�<�2'����N�WֆЅ)j���X3��S��+���X�� s��?mɒ��΁�
[O&�֫��c�?��мu���|v�M���w}{f��ؿ>��߇f�.���!���ƫ�<PJ� +��n         -   x�3�4�2�4�2紴4�2�4�2�iN#c.3NC�=... j�      	   o   x�˩��4�4202�50�54FbrUUp��8c��bfX����r2�0ԁ���%e�`�3AC0�2�+1ԃ�����1���/���Y`�1�?+��n����� ˭J.      
   :  x�u��JA���yq�����*o$V�(P�*#�4E��Z)�U	�$H�,_f���[4.�Q��s������7V^cH�3(J4��E�i��"gp�=C,
��
x����t)��nwn�����q_��@�+�����.�,Y��7�Tk�nR�s|���+a�ț&���n��g���4e��CP��FU�� U�^�	�UUJL���������;#�C���/��4��i��9�l�J@�:7�4��TD���\�����)yթ<�X̂���������5�̖	�lߑ������D&{��G�Fc_�ܑ         k   x���I�0 �s��Y�;�*BҼ�O0�9̘[��)I��r��H	)e�U`̹���C#k�Pcdi"��J�j��>>��|U��GG9}d������ .�~U     